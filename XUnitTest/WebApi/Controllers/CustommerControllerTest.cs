using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers;
public class CustomerControllerTest
{
    private Mock<IService<CustomerDto>> mockCustomerService;
    private CustomerController controller;
    private void SetupBearerToken(Guid userId)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
        var identity = new ClaimsIdentity(claims, "UserId");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers["Authorization"] =
            "Bearer " + Usings.GenerateJwtToken(userId);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    public CustomerControllerTest()
    {
        mockCustomerService = new Mock<IService<CustomerDto>>();
        controller = new CustomerController(mockCustomerService.Object);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Customer_Found()
    {
        // Arrange        
        var customerId = Guid.NewGuid();
        var expectedCustomerDto = new CustomerDto { Id = customerId, Name = "John Doe", Email = "john@example.com" };
        mockCustomerService.Setup(service => service.FindById(customerId)).Returns(expectedCustomerDto);
        SetupBearerToken(customerId);

        // Act
        var result = controller.FindById() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<CustomerDto>(result.Value);
        Assert.Equal(expectedCustomerDto, result.Value);
    }

    [Fact]
    public void FindById_Returns_NotFound_Result_When_Customer_Not_Found()
    {
        // Arrange        
        var customerId = Guid.NewGuid();
        mockCustomerService.Setup(service => service.FindById(customerId)).Returns((CustomerDto)null);
        SetupBearerToken(customerId);

        // Act
        var result = controller.FindById() as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validCustomerDto = new CustomerDto { Name = "John Doe", Email = "john@example.com" };
        mockCustomerService.Setup(service => service.Create(validCustomerDto)).Returns(validCustomerDto);

        // Act
        var result = controller.Create(validCustomerDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<CustomerDto>(result.Value);
        Assert.Equal(validCustomerDto, result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange       
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Create(It.IsAny<CustomerDto>()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validCustomerDto = MockCustomer.GetDtoFromCustomer(MockCustomer.GetFaker());
        mockCustomerService.Setup(service => service.Update(validCustomerDto)).Returns(validCustomerDto);
        SetupBearerToken(validCustomerDto.Id);
        // Act
        var result = controller.Update(validCustomerDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<CustomerDto>(result.Value);
        Assert.Equal(validCustomerDto, result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        SetupBearerToken(Guid.NewGuid());
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Update(It.IsAny<CustomerDto>()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var mockCustomerDto = MockCustomer.GetDtoFromCustomer(MockCustomer.GetFaker());
        mockCustomerService.Setup(service => service.Delete(It.IsAny<CustomerDto>())).Returns(true);
        mockCustomerService.Setup(service => service.FindById(mockCustomerDto.Id)).Returns(mockCustomerDto);
        SetupBearerToken(mockCustomerDto.Id);

        // Act
        var result = controller.Delete(mockCustomerDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)result.Value);
        mockCustomerService.Verify(b => b.Delete(It.IsAny<CustomerDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        SetupBearerToken(Guid.NewGuid());
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Delete((CustomerDto)null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockCustomerService.Verify(b => b.Delete(It.IsAny<CustomerDto>()), Times.Never);
    }
}