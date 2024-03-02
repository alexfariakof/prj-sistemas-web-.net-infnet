using Microsoft.Extensions.DependencyInjection;
using Application.Account;
using Application.Account.Dto;
using Application.Account.Interfaces;

namespace Application.CommonInjectDependence;
public static class ServiceInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IMerchantService, MerchantService>();
        services.AddScoped<IService<CustomerDto>, CustomerService>();
        services.AddScoped<IService<MerchantDto>, MerchantService>();
        return services;
    }
}
