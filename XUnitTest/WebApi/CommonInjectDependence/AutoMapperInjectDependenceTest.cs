using Application.Account.Profile;
using Application.Transactions.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.CommonInjectDependence;
public class AutoMapperInjectDependenceTest
{
    [Fact]
    public void AddAutoMapper_Should_Register_Profiles()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAutoMapper();

        // Assert
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(CustomerProfile)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(MerchantProfile)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(AddressProfile)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(CardProfile)));
    }
}
