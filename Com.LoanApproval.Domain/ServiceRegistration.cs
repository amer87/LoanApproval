using Com.LoanApproval.Domain.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Com.LoanApproval.Domain;

public static class ServiceRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<ILoanRule, LoanAmountRangeRule>();
        services.AddSingleton<ILoanRule, HighValueLoanRule>();
        services.AddSingleton<ILoanRule, LowValueLoanRule>();
        return services;
    }
}
