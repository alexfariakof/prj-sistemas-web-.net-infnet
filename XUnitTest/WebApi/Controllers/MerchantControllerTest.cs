using Application;
using Application.Account.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Controllers;

public class MerchantControllerTest
{
    [Fact]
    public void FindById_Returns_Ok_Result_When_Merchant_Found()
    {
        // Arrange
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        var merchantId = Guid.NewGuid();
        var expectedMerchantDto = new MerchantDto { Id = merchantId, Name = "John Doe", Email = "john@example.com" };

        mockMerchantService.Setup(service => service.FindById(merchantId)).Returns(expectedMerchantDto);

        var controller = new MerchantController(mockMerchantService.Object);

        // Act
        var result = controller.FindById(merchantId) as OkObjectResult;

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
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        var merchantId = Guid.NewGuid();
        mockMerchantService.Setup(service => service.FindById(merchantId)).Returns((MerchantDto)null);

        var controller = new MerchantController(mockMerchantService.Object);

        // Act
        var result = controller.FindById(merchantId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        var validMerchantDto = new MerchantDto { Name = "John Doe", Email = "john@example.com" };

        mockMerchantService.Setup(service => service.Create(validMerchantDto)).Returns(validMerchantDto);

        var controller = new MerchantController(mockMerchantService.Object);

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
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        
        var controller = new MerchantController(mockMerchantService.Object);
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
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        var validMerchantDto = new MerchantDto { Id = Guid.NewGuid(), Name = "Updated John Doe", Email = "updatedjohn@example.com" };

        mockMerchantService.Setup(service => service.Update(validMerchantDto)).Returns(validMerchantDto);

        var controller = new MerchantController(mockMerchantService.Object);

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
        var mockMerchantService = new Mock<IService<MerchantDto>>();

        var controller = new MerchantController(mockMerchantService.Object);
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
        var mockMerchantService = new Mock<IService<MerchantDto>>();
        var mockMerchantDto = new MerchantDto { Name = "John Doe", Email = "john@example.com" };
        mockMerchantService.Setup(service => service.Delete(It.IsAny<MerchantDto>())).Returns(true);
        mockMerchantService.Setup(service => service.FindById(mockMerchantDto.Id)).Returns(mockMerchantDto);

        var controller = new MerchantController(mockMerchantService.Object);

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
        var mockMerchantService = new Mock<IService<MerchantDto>>();

        var controller = new MerchantController(mockMerchantService.Object);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Delete((MerchantDto)null);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockMerchantService.Verify(b => b.Delete(It.IsAny<MerchantDto>()), Times.Never);
    }
}