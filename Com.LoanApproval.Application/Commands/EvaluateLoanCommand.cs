using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

public class EvaluateLoanCommand(LoanApplication application) : IRequest<RuleResult>
{
    public LoanApplication Application { get; set; } = application;
}
