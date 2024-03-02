using Application.Account.Dto;
using Application.Account.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers;
public class AuthControllerTest
{
    private readonly Mock<ICustomerService> mockCustomerService;
    private readonly Mock<IMerchantService> mockMerchantService;
    private readonly AuthController mockController;
    public AuthControllerTest()
    {
        mockCustomerService = new Mock<ICustomerService>();
        mockMerchantService = new Mock<IMerchantService>();
        mockController = new AuthController(mockCustomerService.Object, mockMerchantService.Object);
    }

    [Fact]
    public void SignIn_Returns_Ok_Result_When_Customer_Found()
    {
        // Arrange
        var expectedAuthenticationDto = new AuthenticationDto { AccessToken = "mockToken" };
        var loginDto = new LoginDto { Email = "customer@example.com", Password = "password", Type = UserType.Customer };
        mockCustomerService.Setup(service => service.Authentication(loginDto)).Returns(expectedAuthenticationDto);

        // Act
        var result = mockController.SignIn(loginDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<AuthenticationDto>(result.Value);
        Assert.Equal(expectedAuthenticationDto, result.Value);
    }

    [Fact]
    public void SignIn_Returns_Ok_Result_When_Merchant_Found()
    {
        // Arrange
        var expectedAuthenticationDto = new AuthenticationDto { AccessToken = "mockToken" };
        var loginDto = new LoginDto { Email = "merchant@example.com", Password = "password", Type = UserType.Merchant };
        mockMerchantService.Setup(service => service.Authentication(loginDto)).Returns(expectedAuthenticationDto);

        // Act
        var result = mockController.SignIn(loginDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<AuthenticationDto>(result.Value);
        Assert.Equal(expectedAuthenticationDto, result.Value);
    }

    [Fact]
    public void SignIn_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        mockController.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = mockController.SignIn(It.IsAny<LoginDto>()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void SignIn_Returns_BadRequest_Result_When_Invalid_UserType()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "invalid@example.com", Password = "password"  };

        // Act
        var result = mockController.SignIn(loginDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Erro ao realizar login!", result.Value);
    }

    [Fact]
    public void SignIn_Returns_BadRequest_Result_When_Authentication_Fails()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "failed@example.com", Password = "password", Type = UserType.Customer };
        mockCustomerService.Setup(service => service.Authentication(loginDto)).Throws(new ArgumentException("Authentication failed"));

        // Act
        var result = mockController.SignIn(loginDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Authentication failed", result.Value);
    }

    [Fact]
    public void SignIn_Returns_BadRequest_Result_When_Exception_Occurs()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "exception@example.com", Password = "password", Type = UserType.Customer };
        mockCustomerService.Setup(service => service.Authentication(loginDto)).Throws(new Exception("Exception_Occurs"));

        // Act
        var result = mockController.SignIn(loginDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Exception_Occurs", result.Value);
    }
}