namespace Domain.DependencyInjection
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {

            return services;
        }
    }
}