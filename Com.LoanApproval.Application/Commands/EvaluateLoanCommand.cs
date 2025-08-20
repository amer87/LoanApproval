using Com.LoanApproval.Domain.Rules;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

public record EvaluateLoanCommand(decimal LoanAmount, decimal AssetValue, int CreditScore) : IRequest<List<RuleResult>>;
