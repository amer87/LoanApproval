using Com.LoanApproval.Domain.Models;

namespace Com.LoanApproval.Domain.Rules;

public class LoanAmountRangeRule : ILoanRule
{
    public int Priority { get; set; } = 1; // Set a default priority for this rule
    private readonly decimal _minLoanAmount = 100_000;
    private readonly decimal _maxLoanAmount = 1_500_000;

    public RuleResult Evaluate(LoanApplication application)
    {
        if (application.LoanAmount < _minLoanAmount || application.LoanAmount > _maxLoanAmount)
            return RuleResult.Failure("Loan amount must be between £100,000 and £1,500,000.");
        return RuleResult.Success("Loan amount rule passed.");
    }
}

