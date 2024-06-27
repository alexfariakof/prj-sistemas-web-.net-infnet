using LiteStreaming.AdministrativeApp.CommonDependenceInject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace AdministrativeApp.CommonInjectDependence;
public class AuthenticationInjectDependenceTest
{
    [Fact]
    public void AddAuthenticationCookeis_ShouldConfigureAuthenticationCorrectly()
    {
        // Arrange
        var services = new ServiceCollection();

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        services.AddSingleton(httpContextAccessorMock.Object);

        // Act
        services.AddAuthenticationCookeis();        
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var options = serviceProvider.GetRequiredService<IOptionsMonitor<CookieAuthenticationOptions>>().Get(CookieAuthenticationDefaults.AuthenticationScheme);

        Assert.Equal("/account", options.LoginPath);
        Assert.Equal("/account/logout", options.LogoutPath);
        Assert.True(options.SlidingExpiration);
    }
}
