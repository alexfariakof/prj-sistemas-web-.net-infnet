using Application;
using Application.Account.Dto;
using Domain.Account.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApi.Controllers;
public class CustomerControllerTest
{
    private Mock<IService<CustomerDto>> mockCustomerService;
    private Mock<IService<PlaylistPersonalDto>> mockPlaylistPersonalService;

    private CustomerController controller;
    public CustomerControllerTest()
    {
        mockCustomerService = new Mock<IService<CustomerDto>>();
        mockPlaylistPersonalService = new Mock<IService<PlaylistPersonalDto>>();
        controller = new CustomerController(mockCustomerService.Object, mockPlaylistPersonalService.Object);
    }

    [Fact]
    public void FindById_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserlType.Merchant);

        // Act
        var result = controller.FindById() as UnauthorizedResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Customer_Found()
    {
        // Arrange        
        var customerId = Guid.NewGuid();
        var expectedCustomerDto = new CustomerDto { Id = customerId, Name = "John Doe", Email = "john@example.com" };
        mockCustomerService.Setup(service => service.FindById(customerId)).Returns(expectedCustomerDto);
        Usings.SetupBearerToken(customerId, controller);

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
        Usings.SetupBearerToken(customerId, controller);

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
        var result = controller.Create(new()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserlType.Merchant);

        // Act
        var result = controller.Update((CustomerDto)null) as UnauthorizedResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validCustomerDto = MockCustomer.Instance.GetDtoFromCustomer(MockCustomer.Instance.GetFaker());
        mockCustomerService.Setup(service => service.Update(validCustomerDto)).Returns(validCustomerDto);
        Usings.SetupBearerToken(validCustomerDto.Id, controller);
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
        Usings.SetupBearerToken(Guid.NewGuid(), controller);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Update(new()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var mockCustomerDto = MockCustomer.Instance.GetDtoFromCustomer(MockCustomer.Instance.GetFaker());
        mockCustomerService.Setup(service => service.Delete(It.IsAny<CustomerDto>())).Returns(true);
        mockCustomerService.Setup(service => service.FindById(mockCustomerDto.Id)).Returns(mockCustomerDto);
        Usings.SetupBearerToken(mockCustomerDto.Id, controller);

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
        Usings.SetupBearerToken(Guid.NewGuid(), controller);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Delete(new());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockCustomerService.Verify(b => b.Delete(It.IsAny<CustomerDto>()), Times.Never);
    }

    [Fact]
    public void FindById_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var customerId = Guid.NewGuid();
        mockCustomerService.Setup(service => service.FindById(customerId)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(customerId, controller);

        // Act
        var result = controller.FindById() as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var invalidCustomerDto = new CustomerDto(); // Invalid DTO to trigger exception in the service
        mockCustomerService.Setup(service => service.Create(invalidCustomerDto)).Throws(new Exception("BadRequest_Erro_Message"));

        // Act
        var result = controller.Create(invalidCustomerDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var validCustomerDto = MockCustomer.Instance.GetDtoFromCustomer(MockCustomer.Instance.GetFaker());
        mockCustomerService.Setup(service => service.Update(validCustomerDto)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(validCustomerDto.Id, controller);

        // Act
        var result = controller.Update(validCustomerDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var mockCustomerDto = MockCustomer.Instance.GetDtoFromCustomer(MockCustomer.Instance.GetFaker());
        mockCustomerService.Setup(service => service.Delete(It.IsAny<CustomerDto>())).Throws(new Exception("BadRequest_Erro_Message"));
        mockCustomerService.Setup(service => service.FindById(mockCustomerDto.Id)).Returns(mockCustomerDto);
        Usings.SetupBearerToken(mockCustomerDto.Id, controller);

        // Act
        var result = controller.Delete(mockCustomerDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }
}
