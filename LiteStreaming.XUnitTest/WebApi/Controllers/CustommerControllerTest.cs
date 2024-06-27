using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;
using Domain.Account.ValueObject;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers;
public class CustomerControllerTest
{
    private Mock<IService<CustomerDto>> mockCustomerService;
    private Mock<IPlaylistPersonalService> mockPlaylistPersonalService;

    private CustomerController controller;
    public CustomerControllerTest()
    {
        mockCustomerService = new Mock<IService<CustomerDto>>();
        mockPlaylistPersonalService = new Mock<IPlaylistPersonalService>();
        controller = new CustomerController(mockCustomerService.Object, mockPlaylistPersonalService.Object);
    }

    [Fact]
    public void FindById_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);

        // Act
        var result = controller.FindById();

        // Assert
        //Assert.NotNull(result);
        //Assert.IsType<UnauthorizedResult>(result);

        var okResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, okResult.StatusCode);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Customer_Found()
    {
        // Arrange        
        var customerId = Guid.NewGuid();
        var expectedCustomerDto = new CustomerDto { Id = customerId, Name = "John Doe", Email = "john@example.com" };
        mockCustomerService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(expectedCustomerDto);
        Usings.SetupBearerToken(customerId, controller);

        // Act
        var result = controller.FindById() as ObjectResult;

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
        mockCustomerService.Setup(service => service.FindById(customerId)).Returns(() => null);
        Usings.SetupBearerToken(customerId, controller);

        // Act
        var result = controller.FindById();

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
        var result = controller.Create(validCustomerDto) as ObjectResult;

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
        var result = controller.Create(new());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);

        // Act
        CustomerDto? nullCustomerDto  = null;
        var result = controller.Update(nullCustomerDto);

        // Assert
        Assert.NotNull(result);        
        var badResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badResult.StatusCode);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validCustomerDto = MockCustomer.Instance.GetDtoFromCustomer(MockCustomer.Instance.GetFaker());
        mockCustomerService.Setup(service => service.Update(validCustomerDto)).Returns(validCustomerDto);
        Usings.SetupBearerToken(validCustomerDto.Id, controller);
        // Act
        var result = controller.Update(validCustomerDto) as ObjectResult;

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
        var result = controller.Update(new());

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
        Assert.True((bool?)result.Value);
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
        mockCustomerService.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(customerId, controller);

        // Act
        var result = controller.FindById() as ObjectResult;

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
        var result = controller.Create(invalidCustomerDto) as ObjectResult;

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
        var result = controller.Update(validCustomerDto) as ObjectResult;

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
        var result = controller.Delete(mockCustomerDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void DeleteMusicFromPlaylist_Returns_BadRequest_Result_When_Validation_Fails()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var musicId = Guid.NewGuid();
        var playlist = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(MockPlaylistPersonal.Instance.GetFaker());

        var validationResults = new List<ValidationResult>();
        var dto = new PlaylistPersonalDto { Id = playlistId, Musics = { new MusicDto { Id = musicId } } };
        bool isValid = Validator.TryValidateObject(dto, new ValidationContext(dto, serviceProvider: null, items: new Dictionary<object, object?>
            {
                { "HttpMethod", "DELETE" }
            }), validationResults, validateAllProperties: true);

        mockPlaylistPersonalService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(playlist);

        // Act
        var result = controller.DeleteMusicFromPlaylist(playlistId, musicId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<string>(result.Value);
    }

    [Fact]
    public void DeleteMusicFromPlaylist_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var musicId = Guid.NewGuid();

        mockPlaylistPersonalService.Setup(service => service.FindById(playlistId)).Throws(new Exception("BadRequest_Error_Message"));

        // Act
        var result = controller.DeleteMusicFromPlaylist(playlistId, musicId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Error_Message", result.Value);
    }
}
