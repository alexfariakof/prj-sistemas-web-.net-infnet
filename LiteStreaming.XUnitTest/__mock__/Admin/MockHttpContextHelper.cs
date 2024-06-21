using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace LiteStreaming.XunitTest.__mock__.Admin;

public static class MockHttpContextHelper
{
    public static void MockClaimsIdentity(Guid userId, string name, string email, ControllerBase controller)
    {
        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, name));
        identity.AddClaim(new Claim(ClaimTypes.Email, email));

        var claimsPrincipal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = "/account"
        };

        // Mock the HttpContext
        var httpContext = new DefaultHttpContext();
        var authenticationServiceMock = new Mock<IAuthenticationService>();

        authenticationServiceMock
            .Setup(x => x.SignInAsync(
                It.IsAny<HttpContext>(),
                It.IsAny<string>(),
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddSingleton(authenticationServiceMock.Object);
        serviceCollection.AddSingleton<UrlEncoder>(UrlEncoder.Default);
        serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        serviceCollection.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
        controller.ControllerContext = new ControllerContext(actionContext);

        controller.HttpContext.RequestServices = serviceCollection.BuildServiceProvider();                         
    }

    public static void MockClaimsIdentitySigned(Guid userId, string name, string email, ControllerBase controller)
    {
        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, name));
        identity.AddClaim(new Claim(ClaimTypes.Email, email));

        var claimsPrincipal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = "/account"
        };

        var httpContext = new DefaultHttpContext();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        authenticationServiceMock.Setup(x => x.SignInAsync(
            It.IsAny<HttpContext>(),
            It.IsAny<string>(),
            It.IsAny<ClaimsPrincipal>(),
            It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddSingleton(authenticationServiceMock.Object);
        serviceCollection.AddSingleton<UrlEncoder>(UrlEncoder.Default);
        serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        serviceCollection.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Add Authentication services
        serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = "/account";
            options.LogoutPath = "/account/logout";
            options.AccessDeniedPath = "/account";
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        httpContext.RequestServices = serviceProvider;
        httpContext.User = claimsPrincipal;
        var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor());
        controller.ControllerContext = new ControllerContext(actionContext);
        var signInTask = controller.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
        signInTask.Wait();

        httpContext.User = claimsPrincipal;
        controller.ControllerContext.HttpContext = httpContext;
    }
}
