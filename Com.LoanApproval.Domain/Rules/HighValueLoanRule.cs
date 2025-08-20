using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

public class HighValueLoanRule : ILoanRule
{
    public int Priority { get; set; } = 3; // Set a default priority for this rule
    private readonly int _minimumCreditScore = 950;
    private readonly decimal _maximumLtv = 0.6m;
    private readonly decimal _highValueThreshold = 1_000_000;

    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount >= _highValueThreshold)
        {
            var ltv = application.LoanAmount / application.AssetValue;
            if (ltv > _maximumLtv || application.CreditScore < _minimumCreditScore)
                return RuleResult.Failure("Loans >= £1M require LTV ≤ 60% and credit score ≥ 950.");
        }
        return RuleResult.Success("High value loan rule passed.");
    }
}
