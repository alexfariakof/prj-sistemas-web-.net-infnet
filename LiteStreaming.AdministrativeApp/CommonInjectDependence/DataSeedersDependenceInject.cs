using DataSeeders;
using DataSeeders.Administrative;

namespace AdministrativeApp.CommonDependenceInject;
public static class DataSeedersDependenceInject
{
    public static void AddDataSeeders(this IServiceCollection services)
    {
        services.AddTransient<IDataSeeder, DataSeederAdministrative>();
    }

    public static void RunDataSeeders(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dataSeeder = services.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedData();
        }
    }
}