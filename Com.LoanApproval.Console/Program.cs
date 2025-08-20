// Program.cs
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Com.LoanApproval.Application.Commands;
using Com.LoanApproval.Application.Queries;
using Com.LoanApproval.Domain;
using Com.LoanApproval.Infrastructure;
using Com.LoanApproval.Application;
using Com.LoanApproval.Domain.Rules;
using Com.LoanApproval.Application.Common.Dtos;

var services = new ServiceCollection();

services.AddLogging();
services.AddApplication();
services.AddInfrastructure();
services.AddDomain();

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

bool shouldContinue;

do
{
    var loanAmount = PromptDecimal("Enter loan amount (GBP): ");
    var assetValue = PromptDecimal("Enter asset value (GBP): ");
    var creditScore = PromptInt("Enter credit score (1-999): ");

    try
    {
        var evaluationResult = await mediator.Send(
            new EvaluateLoanCommand(loanAmount, assetValue, creditScore)
        );

        PrintEvaluationResults(evaluationResult);

        var stats = await mediator.Send(new GetLoanStatisticsQuery());
        PrintStatistics(stats);
    }
    catch (ValidationException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Validation errors:");
        foreach (var error in ex.Errors)
            Console.WriteLine($" - {error.ErrorMessage}");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.ResetColor();
    }
    finally
    {
        Console.WriteLine("##########################################");
        shouldContinue = PromptYesNo("Continue? (y/n)");
    }

} while (shouldContinue);


/// <summary>
/// Prompts for a decimal number until valid.
/// </summary>
static decimal PromptDecimal(string label)
{
    while (true)
    {
        Console.Write(label);
        if (decimal.TryParse(Console.ReadLine(), out decimal value))
            return value;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid number. Please enter a valid decimal value.");
        Console.ResetColor();
    }
}

/// <summary>
/// Prompts for an integer until valid.
/// </summary>
static int PromptInt(string label)
{
    while (true)
    {
        Console.Write(label);
        if (int.TryParse(Console.ReadLine(), out int value))
            return value;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid number. Please enter a valid integer.");
        Console.ResetColor();
    }
}

/// <summary>
/// Prompts for a yes/no answer.
/// </summary>
static bool PromptYesNo(string label)
{
    Console.Write(label);
    var response = Console.ReadLine()?.Trim().ToLower();
    return response != null && response.StartsWith('y');
}

/// <summary>
/// Prints loan evaluation results.
/// </summary>
static void PrintEvaluationResults(IEnumerable<RuleResult> results)
{
    Console.WriteLine("##########################################");
    Console.WriteLine("########## Evaluation Results: ##########");
    foreach (var result in results)
    {
        Console.WriteLine(result.IsSuccess
            ? $"Rule Passed: {result.Message}"
            : $"Rule Failed: {result.Message}");
    }
    Console.WriteLine(results.All(r => r.IsSuccess) ? "Loan Approved" : "Loan Declined");
    Console.WriteLine("##########################################");
}

/// <summary>
/// Prints loan statistics.
/// </summary>
static void PrintStatistics(LoanStatisticsDto stats)
{
    Console.WriteLine($"Approved: {stats.ApprovedCount}, Declined: {stats.DeclinedCount}, " +
                      $"Total Approved Value: £{stats.TotalApprovedValue}, Avg LTV: {stats.AverageLTV:P2}");
}
