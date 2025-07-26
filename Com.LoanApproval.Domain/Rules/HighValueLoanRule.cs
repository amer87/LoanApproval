using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

public class HighValueLoanRule : ILoanRule
{
    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount >= 1000000)
        {
            var ltv = application.LoanAmount / application.AssetValue;
            if (ltv > 0.6m || application.CreditScore < 950)
                return RuleResult.Failure("Loans >= £1M require LTV ≤ 60% and credit score ≥ 950.");
        }
        return RuleResult.Success();
    }
}
