using Microsoft.Extensions.DependencyInjection;
using Application.Account;
using Application.Account.Dto;
using Application.Account.Interfaces;
using Application.Streaming.Dto;
using Application.Streaming;

namespace Application.CommonInjectDependence;
public static class ServiceInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IMerchantService, MerchantService>();
        services.AddScoped<IService<CustomerDto>, CustomerService>();
        services.AddScoped<IService<MerchantDto>, MerchantService>();
        services.AddScoped<IService<BandDto>, BandService>();
        services.AddScoped<IService<MusicDto>, MusicService>();
        services.AddScoped<IService<PlaylistDto>, PlaylistService>();
        return services;
    }
}

