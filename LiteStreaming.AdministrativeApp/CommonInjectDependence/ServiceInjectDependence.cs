namespace AdministrativeApp.CommonDependenceInject;
public static class ServiceInjectDependence
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); 
            options.Cookie.HttpOnly = true; 
            options.Cookie.IsEssential = true;
        });
        return services;
    }

    
}

