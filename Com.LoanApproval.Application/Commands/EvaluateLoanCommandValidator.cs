using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using FluentValidation;
using MediatR;

namespace Com.LoanApproval.Application.Commands;

public class EvaluateLoanCommandValidator : AbstractValidator<EvaluateLoanCommand>
{
    public EvaluateLoanCommandValidator()
    {
        RuleFor(command => command.LoanAmount)
            .GreaterThan(0).WithMessage("Loan amount must be greater than zero.");

        RuleFor(command => command.AssetValue)
            .GreaterThan(0).WithMessage("Asset value must be greater than zero.");

        RuleFor(command => command.CreditScore)
            .InclusiveBetween(1, 999).WithMessage("Credit score must be between 1 and 999.");

        RuleFor(command => command)
            .Must(command => command.LoanAmount <= command.AssetValue)
            .WithMessage("Loan amount cannot exceed asset value.");
    }
}
