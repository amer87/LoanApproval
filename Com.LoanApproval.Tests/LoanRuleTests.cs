using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;

namespace Com.LoanApproval.Tests;

public class LoanRuleTests
{
    [Theory]
    // General Limits - Decline cases
    [InlineData(50_000, 200_000, 800, false)] // Below min loan (< £100k)
    [InlineData(99_999, 200_000, 800, false)] // Just below min loan
    [InlineData(1_500_001, 2_000_000, 950, false)] // Just above max loan
    [InlineData(2_000_000, 2_000_000, 950, false)] // Above max loan (> £1.5M)

    // General Limits - Approve cases (boundary)
    [InlineData(100_000, 200_000, 800, true)] // Exactly min loan, LTV=50%, credit=800
    [InlineData(1_500_000, 2_500_000, 950, true)] // Exactly max loan, LTV=60%, credit=950

    // High Value Loans (≥ £1M) - Decline cases
    [InlineData(1_000_000, 1_500_000, 949, false)] // LTV=66.67%, credit below 950
    [InlineData(1_000_000, 1_600_000, 950, false)] // LTV=62.5% (> 60%), credit=950
    [InlineData(1_000_000, 1_666_666, 950, false)] // LTV=60.0001% (> 60%), credit=950
    [InlineData(1_200_000, 1_500_000, 950, false)] // LTV=80% (> 60%), credit=950

    // High Value Loans (≥ £1M) - Approve cases
    [InlineData(1_000_000, 1_666_667, 950, true)] // LTV=60% exactly, credit=950
    [InlineData(1_000_000, 2_000_000, 950, true)] // LTV=50% (< 60%), credit=950
    [InlineData(1_200_000, 2_000_000, 951, true)] // LTV=60%, credit > 950

    // Low Value Loans (< £1M) - LTV < 60% cases
    [InlineData(500_000, 1_000_000, 749, false)] // LTV=50%, credit below 750
    [InlineData(500_000, 1_000_000, 750, true)]  // LTV=50%, credit exactly 750
    [InlineData(599_999, 1_000_000, 800, true)]  // LTV=59.9999%, credit=800

    // Low Value Loans (< £1M) - LTV 60% to < 80% cases
    [InlineData(600_000, 1_000_000, 799, false)] // LTV=60%, credit below 800
    [InlineData(600_000, 1_000_000, 800, true)]  // LTV=60%, credit exactly 800
    [InlineData(700_000, 1_000_000, 850, true)]  // LTV=70%, credit > 800
    [InlineData(799_999, 1_000_000, 800, true)]  // LTV=79.9999%, credit=800

    // Low Value Loans (< £1M) - LTV 80% to < 90% cases
    [InlineData(800_000, 1_000_000, 899, false)] // LTV=80%, credit below 900
    [InlineData(800_000, 1_000_000, 900, true)]  // LTV=80%, credit exactly 900
    [InlineData(850_000, 1_000_000, 950, true)]  // LTV=85%, credit > 900
    [InlineData(899_999, 1_000_000, 900, true)]  // LTV=89.9999%, credit=900

    // Low Value Loans (< £1M) - LTV ≥ 90% cases (Always decline)
    [InlineData(900_000, 1_000_000, 900, false)] // LTV=90% exactly
    [InlineData(900_000, 1_000_000, 950, false)] // LTV=90%, high credit (still decline)
    [InlineData(950_000, 1_000_000, 999, false)] // LTV=95%, max credit (still decline)
    [InlineData(999_999, 1_000_000, 950, false)] // LTV=99.9999%
    public void LoanRules_ApprovalLogic(decimal loanAmount, decimal assetValue, int creditScore, bool expected)
    {
        var application = new LoanApplication
        {
            LoanAmount = loanAmount,
            AssetValue = assetValue,
            CreditScore = creditScore
        };

        var rules = new ILoanRule[]
        {
            new LoanAmountRangeRule(),
            new HighValueLoanRule(),
            new LowValueLoanRule()
        };

        var result = rules.Select(r => r.Evaluate(application)).FirstOrDefault(r => !r.IsSuccess);
        var approved = result == null;

        Assert.Equal(expected, approved);
    }
}