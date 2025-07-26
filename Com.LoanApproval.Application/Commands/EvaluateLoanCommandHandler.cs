using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

public class EvaluateLoanCommandHandler(IEnumerable<ILoanRule> rules) : IRequestHandler<EvaluateLoanCommand, RuleResult>
{
    private readonly IEnumerable<ILoanRule> _rules = rules;

    public Task<RuleResult> Handle(EvaluateLoanCommand request, CancellationToken cancellationToken)
    {
        foreach (var rule in _rules)
        {
            var result = rule.Evaluate(request.Application);
            if (!result.IsSuccess)
                return Task.FromResult(result);
        }
        return Task.FromResult(RuleResult.Success());
    }
}
