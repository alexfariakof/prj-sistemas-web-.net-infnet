using Application.Account;

namespace WebApi.CommonInjectDependence;
public static class ServiceInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<CustomerService>();
        services.AddScoped<MerchantService>();
        return services;
    }
}
