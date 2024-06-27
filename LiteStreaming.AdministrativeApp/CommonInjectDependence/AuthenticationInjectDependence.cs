using Microsoft.AspNetCore.Authentication.Cookies;

namespace LiteStreaming.AdministrativeApp.CommonDependenceInject;
public static class AuthenticationInjectDependence
{
    public static IServiceCollection AddAuthenticationCookeis(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;            
        }).AddCookie(options =>
        {
            options.LoginPath = "/account";
            options.AccessDeniedPath = "/account";
            options.LogoutPath = "/account/logout";
            options.SlidingExpiration = true;
        });

  
        return services;
    }    
}

