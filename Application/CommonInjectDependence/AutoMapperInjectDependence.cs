using Microsoft.Extensions.DependencyInjection;
using Application.Account.Profile;
using Application.Transactions.Profile;

namespace Application.CommonInjectDependence;
public static class AutoMapperInjectDependence
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CustomerProfile).Assembly);
        services.AddAutoMapper(typeof(MerchantProfile).Assembly);
        services.AddAutoMapper(typeof(AddressProfile).Assembly);
        services.AddAutoMapper(typeof(CardProfile).Assembly);
        return services;
    }
}