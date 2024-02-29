using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers;

public class CustomerControllerTest
{
    [Fact]
    public void FindById_Returns_Ok_Result_When_Customer_Found()
    {
        // Arrange
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        var customerId = Guid.NewGuid();
        var expectedCustomerDto = new CustomerDto { Id = customerId, Name = "John Doe", Email = "john@example.com" };

        mockCustomerService.Setup(service => service.FindById(customerId)).Returns(expectedCustomerDto);

        var controller = new CustomerController(mockCustomerService.Object);

        // Act
        var result = controller.FindById(customerId) as OkObjectResult;

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
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        var customerId = Guid.NewGuid();
        mockCustomerService.Setup(service => service.FindById(customerId)).Returns((CustomerDto)null);

        var controller = new CustomerController(mockCustomerService.Object);

        // Act
        var result = controller.FindById(customerId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        var validCustomerDto = new CustomerDto { Name = "John Doe", Email = "john@example.com" };

        mockCustomerService.Setup(service => service.Create(validCustomerDto)).Returns(validCustomerDto);

        var controller = new CustomerController(mockCustomerService.Object);

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
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        
        var controller = new CustomerController(mockCustomerService.Object);
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
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        var validCustomerDto = new CustomerDto { Id = Guid.NewGuid(), Name = "Updated John Doe", Email = "updatedjohn@example.com" };

        mockCustomerService.Setup(service => service.Update(validCustomerDto)).Returns(validCustomerDto);

        var controller = new CustomerController(mockCustomerService.Object);

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
        var mockCustomerService = new Mock<IService<CustomerDto>>();

        var controller = new CustomerController(mockCustomerService.Object);
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
        var mockCustomerService = new Mock<IService<CustomerDto>>();
        var mockCustomerDto = new CustomerDto { Name = "John Doe", Email = "john@example.com" };
        mockCustomerService.Setup(service => service.Delete(It.IsAny<CustomerDto>())).Returns(true);
        mockCustomerService.Setup(service => service.FindById(mockCustomerDto.Id)).Returns(mockCustomerDto);

        var controller = new CustomerController(mockCustomerService.Object);

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
        var mockCustomerService = new Mock<IService<CustomerDto>>();

        var controller = new CustomerController(mockCustomerService.Object);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Delete((CustomerDto)null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockCustomerService.Verify(b => b.Delete(It.IsAny<CustomerDto>()), Times.Never);
    }
}