﻿using __mock__.Admin;
using LiteStreaming.AdministrativeApp.Models;
using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using LiteStreaming.AdministrativeApp.Controllers;
using LiteStreaming.XunitTest.__mock__.Admin;

namespace AdministrativeApp.Controllers;

public class UserControllerTest
{
    private readonly Mock<IAdministrativeAccountService> administrativeAccountServiceMock;
    private readonly UserController userController;

    public UserControllerTest()
    {
        administrativeAccountServiceMock = new Mock<IAdministrativeAccountService>();
        userController = new UserController(administrativeAccountServiceMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = Usings.MockHttpContext(),
            }
        };
    }

    [Fact]
    public void Index_Returns_ViewResult_With_Model()
    {
        // Arrange
        var accounts = MockAdministrativeAccount.Instance.GetFakerListDto();
        administrativeAccountServiceMock.Setup(service => service.FindAll()).Returns(accounts);

        // Act
        var result = userController.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<AdministrativeAccountDto>>(viewResult.Model);
        Assert.Equal(accounts, model);
    }

    [Fact]
    public void Create_Returns_ViewResult()
    {
        // Act
        MockHttpContextHelper.MockClaimsIdentitySigned(Guid.NewGuid(), "teste", "teste@teste.com", userController);
        var result = userController.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Save_ModelStateInvalid_Returns_CreateView()
    {
        // Arrange
        userController.ModelState.AddModelError("test", "test error");

        // Act
        var result = userController.Save(new AdministrativeAccountDto());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Create", viewResult.ViewName);
    }

    [Fact]
    public void Save_ValidDto_RedirectsToIndex()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();        
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);

        // Act
        var result = userController.Save(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        administrativeAccountServiceMock.Verify(service => service.Create(dto), Times.Once);
    }

    [Fact]
    public void Save_ServiceThrowsArgumentException_Returns_CreateView_WithWarningAlert()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);
        administrativeAccountServiceMock.Setup(service => service.Create(It.IsAny<AdministrativeAccountDto>())).Throws(new ArgumentException("Invalid data."));

        // Act
        var result = userController.Save(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Create", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Informação", alert.Header);
        Assert.Equal("warning", alert.Type);
        Assert.Equal("Invalid data.", alert.Message);
    }

    [Fact]
    public void Save_ServiceThrowsException_Returns_CreateView_WithDangerAlert()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);
        administrativeAccountServiceMock.Setup(service => service.Create(It.IsAny<AdministrativeAccountDto>())).Throws(new Exception("Error"));

        // Act
        var result = userController.Save(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Create", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao salvar os dados do usuário.", alert.Message);
    }

    [Fact]
    public void Update_ModelStateInvalid_Returns_EditView()
    {
        // Arrange
        userController.ModelState.AddModelError("test", "test error");

        // Act
        var result = userController.Update(new AdministrativeAccountDto());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Edit", viewResult.ViewName);
    }

    [Fact]
    public void Update_ValidDto_RedirectsToIndex()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);

        // Act
        var result = userController.Update(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        administrativeAccountServiceMock.Verify(service => service.Update(dto), Times.Once);
    }

    [Fact]
    public void Update_ServiceThrowsArgumentException_Returns_EditView_WithWarningAlert()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);
        administrativeAccountServiceMock.Setup(service => service.Update(It.IsAny<AdministrativeAccountDto>())).Throws(new ArgumentException("Invalid data."));

        // Act
        var result = userController.Update(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Edit", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Informação", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Invalid data.", alert.Message);
    }

    [Fact]
    public void Update_ServiceThrowsException_Returns_EditView_WithDangerAlert()
    {
        // Arrange
        var dto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(dto.UsuarioId, dto.Name, dto.Email, userController);
        administrativeAccountServiceMock.Setup(service => service.Update(It.IsAny<AdministrativeAccountDto>())).Throws(new Exception("Error"));

        // Act
        var result = userController.Update(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Edit", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal($"Ocorreu um erro ao atualizar os dados do usuário {dto?.Name}.", alert.Message);
    }

    [Fact]
    public void Edit_ValidId_Returns_ViewResult_With_Model()
    {
        // Arrange        
        var accountDto = MockAdministrativeAccount.Instance.GetFakerDto();
        var id = accountDto.Id;
        administrativeAccountServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(accountDto);

        // Act
        var result = userController.Edit(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<AdministrativeAccountDto>(viewResult.Model);
        Assert.Equal(accountDto, model);
    }

    [Fact]
    public void Edit_ServiceThrowsArgumentException_Returns_IndexView_WithWarningAlert()
    {
        // Arrange
        var id = Guid.NewGuid();
        administrativeAccountServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new ArgumentException("Invalid user."));

        // Act
        var result = userController.Edit(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Informação", alert.Header);
        Assert.Equal("warning", alert.Type);
        Assert.Equal("Invalid user.", alert.Message);
    }

    [Fact]
    public void Edit_ServiceThrowsException_Returns_IndexView_WithDangerAlert()
    {
        // Arrange
        var id = Guid.NewGuid();
        administrativeAccountServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new Exception("Error"));

        // Act
        var result = userController.Edit(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao editar os dados deste usuário.", alert.Message);
    }

    [Fact]
    public void Delete_ValidId_Returns_IndexView_With_SuccessAlert()
    {
        // Arrange     
        var accountDto = MockAdministrativeAccount.Instance.GetFakerDto();
        MockHttpContextHelper.MockClaimsIdentitySigned(accountDto.UsuarioId, accountDto.Name, accountDto.Email, userController);
        administrativeAccountServiceMock.Setup(service => service.Delete(It.IsAny<AdministrativeAccountDto>())).Returns(true);
        
        // Act
        var result = userController.Delete(accountDto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Sucesso", alert.Header);
        Assert.Equal("success", alert.Type);
        Assert.Equal($"Usuário {accountDto?.Name} excluído.", alert.Message);
    }

    [Fact]
    public void Delete_ServiceThrowsArgumentException_Returns_IndexView_WithWarningAlert()
    {
        // Arrange
        MockHttpContextHelper.MockClaimsIdentitySigned(Guid.NewGuid(), "teste", "teste@teste.com", userController);
        administrativeAccountServiceMock.Setup(service => service.Delete(It.IsAny<AdministrativeAccountDto>())).Throws(new ArgumentException("Invalid user."));

        // Act
        var result = userController.Delete(new());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Informação", alert.Header);
        Assert.Equal("warning", alert.Type);
        Assert.Equal("Invalid user.", alert.Message);
    }

    [Fact]
    public void Delete_ServiceThrowsException_Returns_IndexView_WithDangerAlert()
    {
        // Arrange
        MockHttpContextHelper.MockClaimsIdentitySigned(Guid.NewGuid(), "teste", "teste@teste.com", userController);
        administrativeAccountServiceMock.Setup(service => service.Delete(It.IsAny<AdministrativeAccountDto>())).Throws(new Exception("Error"));

        // Act
        var result = userController.Delete(new());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao excluir o usuário .", alert.Message);
    }
}
