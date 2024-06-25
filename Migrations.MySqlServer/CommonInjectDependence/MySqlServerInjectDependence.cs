using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations.MySqlServer.CommonInjectDependence;
public static class MySqlServerInjectDependence
{
    public static IServiceCollection MySqlServerMigrationsApplicationContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MySqlServerContext).Assembly.FullName;
        services.AddDbContext<MySqlServerContext>(options => options.UseLazyLoadingProxies().UseMySQL(
            configuration.GetConnectionString("MySqlConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        return services;
    }

    public static IServiceCollection MySqlServerMigrationsAdministrativeContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MySqlServerContextAdministrative).Assembly.FullName;
        services.AddDbContext<MySqlServerContextAdministrative>(options => options.UseLazyLoadingProxies().UseMySQL(
            configuration.GetConnectionString("MySqlAdministrativeConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        return services;
    }
}