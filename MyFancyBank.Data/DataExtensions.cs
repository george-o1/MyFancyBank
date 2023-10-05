using Microsoft.Extensions.DependencyInjection;
using MyFancyBank.Data.Interfaces;
using MyFancyBank.Data.Repositories;

namespace MyFancyBank.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepository, CustomerRepository>();
            return services;
        }
    }
}
