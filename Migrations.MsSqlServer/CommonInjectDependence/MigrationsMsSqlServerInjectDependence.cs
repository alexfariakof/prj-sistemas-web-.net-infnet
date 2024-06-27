using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations.MsSqlServer.CommonInjectDependence;
public static class MigrationsMsSqlServerInjectDependence
{
    public static IServiceCollection MsSqlServerMigrationsApplicationContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MsSqlServerContext).Assembly.FullName;
        services.AddDbContext<MsSqlServerContext>(options => options.UseLazyLoadingProxies().UseSqlServer(
            configuration.GetConnectionString("MsSqlConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        return services;
    }

    public static IServiceCollection MsSqlServerMigrationsAdministrativeContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MsSqlServerContextAdministrative).Assembly.FullName;
        services.AddDbContext<MsSqlServerContextAdministrative>(options => options.UseLazyLoadingProxies().UseSqlServer(
            configuration.GetConnectionString("MsSqlAdministrativeConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        return services;
    }
}