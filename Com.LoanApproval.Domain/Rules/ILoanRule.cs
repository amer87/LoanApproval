using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

// TODO : Use proper rule engine
public interface ILoanRule
{
    public int Priority { get; set; }
    RuleResult Evaluate(LoanApplication application);
}
