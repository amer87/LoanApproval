using Xunit;
using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;

namespace Com.LoanApproval.Tests;

public class LoanRuleTests
{
    [Theory]
    [InlineData(50000, 200000, 800, false)] // Below min loan
    [InlineData(2000000, 2000000, 999, false)] // Above max loan
    [InlineData(1000000, 1500000, 949, false)] // High value, low credit
    [InlineData(1000000, 1500000, 950, false)] // High value, LTV > 60% (should decline)
    [InlineData(900000, 1000000, 700, false)] // LTV < 60%, low credit
    [InlineData(900000, 1000000, 800, false)] // LTV = 90%, should decline
    [InlineData(900000, 950000, 950, false)] // LTV >= 90%
    [InlineData(900000, 1000000, 900, false)] // LTV = 90%, should decline

    // Positive cases
    [InlineData(1000000, 2000000, 950, true)] // High value, LTV = 50%, credit = 950
    [InlineData(900000, 2000000, 800, true)] // LTV < 60%, credit = 800
    [InlineData(900000, 1200000, 800, true)] // LTV < 80%, credit = 800
    public void LoanRules_ApprovalLogic(decimal loanAmount, decimal assetValue, int creditScore, bool expected)
    {
        var application = new LoanApplication { LoanAmount = loanAmount, AssetValue = assetValue, CreditScore = creditScore };
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
