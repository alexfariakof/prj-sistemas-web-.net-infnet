using __mock__.Admin;
using AdministrativeApp.Models;
using Application.Administrative.Interfaces;
using Application.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdministrativeApp.Controllers;
public class HomeControllerTest
{
    private readonly Mock<ILogger<HomeController>> loggerMock;
    private readonly Mock<IAuthenticationService> authenticationServiceMock;
    private readonly HomeController homeController;

    public HomeControllerTest()
    {
        loggerMock = new Mock<ILogger<HomeController>>();
        authenticationServiceMock = new Mock<IAuthenticationService>();
        homeController = new HomeController(loggerMock.Object, authenticationServiceMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = Usings.MockHttpContext(),
            }
        };
    }

    [Fact]
    public void Index_Returns_ViewResult()
    {
        // Act
        var result = homeController.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Privacy_Returns_ViewResult()
    {
        // Act
        var result = homeController.Privacy();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Error_Returns_ViewResult_With_ErrorViewModel()
    {
        // Arrange
        homeController.ControllerContext.HttpContext.TraceIdentifier = "test_trace_id";

        // Act
        var result = homeController.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.Equal("test_trace_id", model.RequestId);
    }

    [Fact]
    public void SingIn_ModelStateInvalid_Returns_IndexView()
    {
        // Arrange
        homeController.ModelState.AddModelError("test", "test error");

        // Act
        var result = homeController.SingIn(new LoginDto());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
    }

    [Fact]
    public void SingIn_AuthenticationSuccessful_RedirectsToIndex()
    {
        // Arrange
        var accountDto = MockAdministrativeAccount.Instance.GetFakerDto();
        accountDto.UsuarioId = accountDto.Id;
        var loginDto = new LoginDto { Email = accountDto.Email, Password = accountDto.Password };
        authenticationServiceMock.Setup(service => service.Authentication(It.IsAny<LoginDto>())).Returns(accountDto);

        // Act
        var result = homeController.SingIn(loginDto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal(accountDto.Id.ToString(), homeController.HttpContext.Session.GetString("UserId"));
        Assert.Equal(accountDto.Name, homeController.HttpContext.Session.GetString("UserName"));
    }

    [Fact]
    public void SingIn_AuthenticationThrowsArgumentException_Returns_IndexView_WithWarningAlert()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
        authenticationServiceMock.Setup(service => service.Authentication(It.IsAny<LoginDto>())).Throws(new ArgumentException("Invalid user."));

        // Act
        var result = homeController.SingIn(loginDto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Informação", alert.Header);
        Assert.Equal("warning", alert.Type);
        Assert.Equal("Invalid user.", alert.Message);
    }

    [Fact]
    public void SingIn_AuthenticationThrowsException_Returns_IndexView_WithDangerAlert()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
        authenticationServiceMock.Setup(service => service.Authentication(It.IsAny<LoginDto>())).Throws(new Exception("Error"));

        // Act
        var result = homeController.SingIn(loginDto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao realizar login.", alert.Message);
    }

    [Fact]
    public void LogOut_ClearsSessionAndRedirectsToIndex()
    {
        // Arrange
        homeController.HttpContext.Session.SetString("UserId", "test_user_id");

        // Act
        var result = homeController.LogOut();

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.NotNull(homeController.HttpContext.Session.GetString("UserId"));
    }
}
