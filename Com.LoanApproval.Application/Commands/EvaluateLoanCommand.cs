using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

// TODO : Add fluent validation for this command

public record EvaluateLoanCommand(decimal LoanAmount, decimal AssetValue, int CreditScore) : IRequest<RuleResult>;
