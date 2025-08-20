using Com.LoanApproval.Domain.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Com.LoanApproval.Domain;

public static class ServiceRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<ILoanRule>()
            .AddClasses(classes => classes.AssignableTo(typeof(ILoanRule)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }
}
