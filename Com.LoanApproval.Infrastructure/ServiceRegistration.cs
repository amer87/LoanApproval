using Com.LoanApproval.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

using Com.LoanApproval.Application.Interfaces;
namespace Com.LoanApproval.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ILoanStatisticsRepository, LoanStatisticsRepository>();
        return services;
    }
}
