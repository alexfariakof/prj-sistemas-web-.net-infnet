using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Repository;
using Repository.Repositories;

namespace WebApi.CommonInjectDependence;
public static class RepositoryInjectDependence
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
        services.AddScoped(typeof(IRepository<Merchant>), typeof(MerchantRepository));
        services.AddScoped(typeof(IRepository<Flat>), typeof(FlatRepository));        
        return services;
    }
}