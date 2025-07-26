using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

// This rules could be separated into a different file if needed,
// but the chance of having overlapping rules is higher.

public class LowValueLoanRule : ILoanRule
{
    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount < 1000000)
        {
            var ltv = application.LoanAmount / application.AssetValue;
            if (ltv < 0.6m && application.CreditScore < 750)
                return RuleResult.Failure("LTV < 60% requires credit score ≥ 750.");
            if (ltv < 0.8m && ltv >= 0.6m && application.CreditScore < 800)
                return RuleResult.Failure("LTV < 80% requires credit score ≥ 800.");
            if (ltv < 0.9m && ltv >= 0.8m && application.CreditScore < 900)
                return RuleResult.Failure("LTV < 90% requires credit score ≥ 900.");
            if (ltv >= 0.9m)
                return RuleResult.Failure("LTV ≥ 90% is not allowed.");
        }
        return RuleResult.Success();
    }
}
