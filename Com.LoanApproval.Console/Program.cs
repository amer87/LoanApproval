// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Com.LoanApproval.Application.Commands;
using Com.LoanApproval.Application.Queries;
using Com.LoanApproval.Domain.Models;
using Com.LoanApproval.Domain.Rules;
using Com.LoanApproval.Infrastructure;
using Com.LoanApproval.Application.Interfaces;

// Create a service collection and configure services
// This includes MediatR for command handling and the LoanStatisticsService for statistics
var services = new ServiceCollection();
services.AddLogging();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EvaluateLoanCommand>());
services.AddInfrastructure();
services.AddSingleton<ILoanRule, LoanAmountRangeRule>();
services.AddSingleton<ILoanRule, HighValueLoanRule>();
services.AddSingleton<ILoanRule, LowValueLoanRule>();
var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();
var statsRepository = provider.GetRequiredService<ILoanStatisticsRepository>();

while (true)
{
    Console.WriteLine("Enter loan amount (GBP):");
    var loanAmount = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter asset value (GBP):");
    var assetValue = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter credit score (1-999):");
    var creditScore = int.Parse(Console.ReadLine());
    var application = new LoanApplication { LoanAmount = loanAmount, AssetValue = assetValue, CreditScore = creditScore };
    var result = await mediator.Send(new EvaluateLoanCommand(application));
    var approved = result.IsSuccess;
    if (approved)
        Console.WriteLine("Loan Approved!");
    else
        Console.WriteLine($"Declined: {result.Message}");
    statsRepository.AddRecord(loanAmount, assetValue, approved);
    var stats = await mediator.Send(new GetLoanStatisticsQuery());
    Console.WriteLine($"Approved: {stats.ApprovedCount}, Declined: {stats.DeclinedCount}, Total Approved Value: £{stats.TotalApprovedValue}, Avg LTV: {stats.AverageLTV:P2}");
    Console.WriteLine("Continue? (y/n)");
    if (!Console.ReadLine().Trim().ToLower().StartsWith("y"))
        break;
}
