using Microsoft.Extensions.DependencyInjection;
using Com.LoanApproval.Application.Common.Interfaces;

namespace Com.LoanApproval.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILoanStatisticsRepository, LoanStatisticsRepository>();
        return services;
    }
}
