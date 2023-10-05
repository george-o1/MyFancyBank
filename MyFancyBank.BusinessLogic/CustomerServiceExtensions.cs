using Microsoft.Extensions.DependencyInjection;
using MyFancyBank.BusinessLogic.Interfaces;
using MyFancyBank.BusinessLogic.Services;

namespace MyFancyBank.BusinessLogic
{
    public static class CustomerServiceExtensions
    {
        public static IServiceCollection AddCustomerServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}
