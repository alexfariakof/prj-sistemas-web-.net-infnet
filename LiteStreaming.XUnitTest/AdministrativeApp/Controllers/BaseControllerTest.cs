﻿using __mock__.Admin;
using LiteStreaming.AdministrativeApp.Controllers.Abstractions;
using LiteStreaming.XunitTest.__mock__.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AdministrativeApp.Controllers;

public class BaseControllerTest
{
    public class MockBaseController : BaseController 
    {
        public IActionResult TestAction(HttpContext httpContext)
        {
            var actionContext = new ActionContext(
                     httpContext,
                     new Microsoft.AspNetCore.Routing.RouteData(),
                     new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                this);

            OnActionExecuting(context);
            return context.Result;
        }
    }

    [Fact]
    public void BaseController_UserId_SuccessfullyRetrieved()
    {
        // Arrange
        var controller = new MockBaseController();
        var account = MockAdministrativeAccount.Instance.GetFaker();
        MockHttpContextHelper.MockClaimsIdentitySigned(account.Id, account.Name, account.Login.Email, controller);

        // Act
        var result = controller.TestAction(controller.HttpContext);

        // Assert
        Assert.Equal(account.Id, controller.UserId);
    }

    [Fact]
    public void BaseController_UserId_ThrowsArgumentNullException()
    {
        // Arrange
        var controller = new MockBaseController();
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal();

        // Act
        var result = Assert.Throws<NullReferenceException>(() => controller.TestAction(httpContext));

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void BaseController_OnActionExecuting_SignOutAndSetLoginError()
    {
        // Arrange
        var controller = new MockBaseController();
        MockHttpContextHelper.MockClaimsIdentity(null, "", "", controller);
         
        // Act        
        var result = controller.TestAction(controller.HttpContext);

        // Assert
        Assert.NotNull(controller.ViewBag.LoginError);
    }
}