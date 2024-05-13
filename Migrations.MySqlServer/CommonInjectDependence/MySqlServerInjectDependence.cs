using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations.MySqlServer.CommonInjectDependence;
public static class MySqlServerInjectDependence
{
    public static IServiceCollection ConfigureMySqlServerMigrationsContext(this IServiceCollection services, IConfiguration configuration)
    {
        var name = typeof(MySqlServerContext).Assembly.FullName;
        services.AddDbContext<MySqlServerContext>(options => options.UseLazyLoadingProxies().UseMySQL(
            configuration.GetConnectionString("MySqlConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        name = typeof(MySqlServerContextAdministravtive).Assembly.FullName;
        services.AddDbContext<MySqlServerContextAdministravtive>(options => options.UseLazyLoadingProxies().UseMySQL(
            configuration.GetConnectionString("MySqlAdministrativeConnectionString"),
            builder => builder.MigrationsAssembly(name)));
        return services;
    }
}