
using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

public class CustomRule : ILoanRule
{
    public int Priority { get; set; } = 1000; // Set a default priority for this rule

    public RuleResult Evaluate(LoanApplication application)
    {
        return RuleResult.Success("Custom rule passed.");
    }
}