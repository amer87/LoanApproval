using Com.LoanApproval.Application.Common.Interfaces;
using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

public class EvaluateLoanCommandHandler(
    IEnumerable<ILoanRule> rules,
    ILoanStatisticsRepository statsRepository)
    : IRequestHandler<EvaluateLoanCommand, List<RuleResult>>
{
    private readonly IEnumerable<ILoanRule> _rules = rules;
    private readonly ILoanStatisticsRepository _statsRepository = statsRepository;

    public Task<List<RuleResult>> Handle(EvaluateLoanCommand request, CancellationToken cancellationToken)
    {
        (var loanAmount, var assetValue, var creditScore) = request;

        var application = new LoanApplication
        {
            LoanAmount = loanAmount,
            AssetValue = assetValue,
            CreditScore = creditScore
        };

        var orderedRules = _rules.OrderBy(r => r.Priority).ToList();
        List<RuleResult> evaluationResult = [.. _rules.Select(rule => rule.Evaluate(application))];
        var isApproved = evaluationResult.All(r => r.IsSuccess);
        _statsRepository.AddRecord(loanAmount, assetValue, isApproved);
        return Task.FromResult(evaluationResult);
    }
}
