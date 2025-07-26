using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

public class LoanAmountRangeRule : ILoanRule
{
    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount < 100000 || application.LoanAmount > 1500000)
            return RuleResult.Failure("Loan amount must be between £100,000 and £1,500,000.");
        return RuleResult.Success();
    }
}

