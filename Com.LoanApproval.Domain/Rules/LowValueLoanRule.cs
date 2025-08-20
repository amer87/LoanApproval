using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

// This rules could be separated into a different file if needed,
// but the chance of having overlapping rules is higher.

public class LowValueLoanRule : ILoanRule
{
    public int Priority { get; set; } = 2; // Set a default priority for this rule
    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount >= 1000000)
            return RuleResult.Success("Low value loan rule passed.");

        if (application.AssetValue <= 0)
            return RuleResult.Failure("Asset value must be greater than zero.");

        var ltv = application.LoanAmount / application.AssetValue;

        return (ltv, application.CreditScore) switch
        {
            ( >= 0.9m, _) => RuleResult.Failure("LTV ≥ 90% is not allowed."),

            ( >= 0.8m and < 0.9m, < 900) => RuleResult.Failure(
                $"LTV between 80%-90% requires credit score ≥ 900 (current: {application.CreditScore})."),

            ( >= 0.6m and < 0.8m, < 800) => RuleResult.Failure(
                $"LTV between 60%-80% requires credit score ≥ 800 (current: {application.CreditScore})."),

            ( < 0.6m, < 750) => RuleResult.Failure(
                $"LTV < 60% requires credit score ≥ 750 (current: {application.CreditScore})."),

            _ => RuleResult.Success("Low value loan rule passed.")
        };
    }
}
