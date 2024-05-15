using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Domain.Account.ValueObject;

namespace WebApi.Controllers;
public class MerchantControllerTest
{
    private Mock<IService<MerchantDto>> mockMerchantService;
    private MerchantController controller;

    public MerchantControllerTest()
    {
        mockMerchantService = new Mock<IService<MerchantDto>>();
        controller = new MerchantController(mockMerchantService.Object);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Merchant_Found()
    {
        // Arrange        
        var expectedMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        Usings.SetupBearerToken(expectedMerchantDto.Id, controller, PerfilUser.UserType.Merchant);
        mockMerchantService.Setup(service => service.FindById(expectedMerchantDto.Id)).Returns(expectedMerchantDto);
        
        // Act
        var result = controller.FindById() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MerchantDto>(result.Value);
        Assert.Equal(expectedMerchantDto, result.Value);
    }

    [Fact]
    public void FindById_Returns_NotFound_Result_When_Merchant_Not_Found()
    {
        // Arrange        
        var expectedMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        Usings.SetupBearerToken(expectedMerchantDto.Id, controller,  PerfilUser.UserType.Merchant);
        mockMerchantService.Setup(service => service.FindById(expectedMerchantDto.Id)).Returns((MerchantDto)null);

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
        var validMerchantDto  = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        Usings.SetupBearerToken(validMerchantDto.Id, controller, PerfilUser.UserType.Merchant);
        mockMerchantService.Setup(service => service.Create(validMerchantDto)).Returns(validMerchantDto);

        // Act
        var result = controller.Create(validMerchantDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MerchantDto>(result.Value);
        Assert.Equal(validMerchantDto, result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Merchant);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");
        // Act
        var result = controller.Create(It.IsAny<MerchantDto>()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        Usings.SetupBearerToken(validMerchantDto.Id, controller, PerfilUser.UserType.Merchant);
        mockMerchantService.Setup(service => service.Update(validMerchantDto)).Returns(validMerchantDto);
        
        // Act
        var result = controller.Update(validMerchantDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MerchantDto>(result.Value);
        Assert.Equal(validMerchantDto, result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange      
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Merchant);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Update(It.IsAny<MerchantDto>()) as BadRequestResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var mockMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        Usings.SetupBearerToken(mockMerchantDto.Id, controller, PerfilUser.UserType.Merchant);
        mockMerchantService.Setup(service => service.Delete(It.IsAny<MerchantDto>())).Returns(true);
        mockMerchantService.Setup(service => service.FindById(mockMerchantDto.Id)).Returns(mockMerchantDto);
        
        // Act
        var result = controller.Delete(mockMerchantDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)result.Value);
        mockMerchantService.Verify(b => b.Delete(It.IsAny<MerchantDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Merchant);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Delete((MerchantDto)null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockMerchantService.Verify(b => b.Delete(It.IsAny<MerchantDto>()), Times.Never);
    }

    [Fact]
    public void FindById_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var merchantId = Guid.NewGuid();
        mockMerchantService.Setup(service => service.FindById(merchantId)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(merchantId, controller, PerfilUser.UserType.Merchant);

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
        var invalidMerchantDto = new MerchantDto(); // Invalid DTO to trigger exception in the service
        mockMerchantService.Setup(service => service.Create(invalidMerchantDto)).Throws(new Exception("BadRequest_Erro_Message"));

        // Act
        var result = controller.Create(invalidMerchantDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var validMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        mockMerchantService.Setup(service => service.Update(validMerchantDto)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(validMerchantDto.Id, controller, PerfilUser.UserType.Merchant);

        // Act
        var result = controller.Update(validMerchantDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var mockMerchantDto = MockMerchant.Instance.GetDtoFromMerchant(MockMerchant.Instance.GetFaker());
        mockMerchantService.Setup(service => service.Delete(It.IsAny<MerchantDto>())).Throws(new Exception("BadRequest_Erro_Message"));
        mockMerchantService.Setup(service => service.FindById(mockMerchantDto.Id)).Returns(mockMerchantDto);
        Usings.SetupBearerToken(mockMerchantDto.Id, controller, PerfilUser.UserType.Merchant);

        // Act
        var result = controller.Delete(mockMerchantDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }
}