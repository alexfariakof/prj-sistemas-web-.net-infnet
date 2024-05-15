using Microsoft.Extensions.DependencyInjection;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Repository.Persistency;
using Repository.Interfaces;
using Domain.Administrative.Agreggates;
using Repository.Interfaces.Administrative;
using Repository.Persistency.Administrative;

namespace Repository.CommonInjectDependence;
public static class RepositoryInjectDependence
{
    public static IServiceCollection AddRepositoriesAdministrativeApp(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<AdministrativeAccount>), typeof(AdminAccountRepository));
        services.AddScoped(typeof(IPerfilRepository), typeof(PerfilRepository));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Administrative 
        services.AddScoped(typeof(IRepository<AdministrativeAccount>), typeof(AdminAccountRepository));
        services.AddScoped(typeof(IPerfilRepository), typeof(PerfilRepository));

        // Application 
        services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
        services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
        services.AddScoped(typeof(IRepository<Merchant>), typeof(MerchantRepository));
        services.AddScoped(typeof(IRepository<Flat>), typeof(FlatRepository));
        services.AddScoped(typeof(IRepository<Band>), typeof(BandRepository));
        services.AddScoped(typeof(IRepository<Music>), typeof(MusicRepository));
        services.AddScoped(typeof(IRepository<Playlist>), typeof(PlaylistRepository));
        services.AddScoped(typeof(IRepository<Album>), typeof(AlbumRepository));
        services.AddScoped(typeof(IRepository<PlaylistPersonal>), typeof(PlaylistPersonalRepository));
        services.AddScoped(typeof(IRepository<Genre>), typeof(GenreRepository));
        services.AddScoped(typeof(ICreditCardBrandRepository), typeof(CreditCardBrandRepository));
        services.AddScoped(typeof(IUserTypeRepository), typeof(UserTypeRepository));

        return services;
    }
}