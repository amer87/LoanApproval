// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Com.LoanApproval.Application.Commands;
using Com.LoanApproval.Application.Queries;
using Com.LoanApproval.Application.Interfaces;
using Com.LoanApproval.Domain;
using Com.LoanApproval.Infrastructure;

var services = new ServiceCollection();
services.AddLogging();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EvaluateLoanCommand>());
services.AddInfrastructure();
services.AddDomain();
var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();
var statsRepository = provider.GetRequiredService<ILoanStatisticsRepository>();

while (true)
{
    Console.WriteLine("Enter loan amount (GBP):");
    // FIXME: Add input validation for decimal parsing
    // Validate numeric inputs
    // Most Importantly to ensure asset value is not zero to avoid division by zero
    var loanAmount = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter asset value (GBP):");
    var assetValue = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter credit score (1-999):");
    var creditScore = int.Parse(Console.ReadLine());

    var command = new EvaluateLoanCommand(loanAmount, assetValue, creditScore);
    var result = await mediator.Send(command);
    var approved = result.IsSuccess;
    Console.WriteLine(approved ? "Loan Approved!" : $"Declined: {result.Message}");

    statsRepository.AddRecord(loanAmount, assetValue, approved);
    var stats = await mediator.Send(new GetLoanStatisticsQuery());
    Console.WriteLine($"Approved: {stats.ApprovedCount}, Declined: {stats.DeclinedCount}, Total Approved Value: £{stats.TotalApprovedValue}, Avg LTV: {stats.AverageLTV:P2}");
    Console.WriteLine("Continue? (y/n)");
    if (!Console.ReadLine().Trim().StartsWith("y", StringComparison.CurrentCultureIgnoreCase))
        break;
}
