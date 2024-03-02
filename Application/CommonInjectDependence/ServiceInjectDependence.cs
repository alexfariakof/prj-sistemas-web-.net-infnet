using Microsoft.Extensions.DependencyInjection;
using Application.Account;
using Application.Account.Dto;

namespace Application.CommonInjectDependence;
public static class ServiceInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<CustomerService>();
        services.AddScoped<MerchantService>();
        services.AddScoped<IService<CustomerDto>, CustomerService>();
        services.AddScoped<IService<MerchantDto>, MerchantService>();
        return services;
    }
}
