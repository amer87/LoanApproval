using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

//TODO: Use proper rule engine
public interface ILoanRule
{
    RuleResult Evaluate(LoanApplication application);
}
