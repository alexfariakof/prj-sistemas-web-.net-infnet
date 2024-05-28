using Microsoft.Extensions.DependencyInjection;
using Application.Account;
using Application.Account.Dto;
using Application.Account.Interfaces;
using Application.Administrative;
using Application.Administrative.Interfaces;
using Microsoft.Extensions.Configuration;
using LiteStreaming.Cryptography;

namespace Application.CommonInjectDependence;
public static class ServiceInjectDependence
{

    public static IServiceCollection AddServicesCryptography(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CryptoOptions>(configuration.GetSection("CryptoConfigurations"));
        services.AddSingleton<ICrypto, Crypto>();
        return services;
    }

    public static IServiceCollection AddServicesAdministrativeApp(this IServiceCollection services)
    {
        services.AddScoped<IAdministrativeAccountService, AdministrativeAccountService>();
        services.AddScoped<IAuthenticationService, AdministrativeAccountService>();        
        return services;
    }

    public static IServiceCollection AddServicesWebApiApp(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IService<CustomerDto>, CustomerService>();
        services.AddScoped<IService<PlaylistPersonalDto>, PlaylistPersonalService>();
        services.AddScoped<IService<MerchantDto>, MerchantService>();
        services.AddScoped<IService<BandDto>, BandService>();
        services.AddScoped<IService<MusicDto>, MusicService>();
        services.AddScoped<IService<PlaylistDto>, PlaylistService>();
        services.AddScoped<IService<AlbumDto>, AlbumService>();        
        return services;
    }
}

