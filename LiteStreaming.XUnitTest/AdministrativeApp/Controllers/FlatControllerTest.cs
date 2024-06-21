﻿using AdministrativeApp.Models;
using Application.Streaming.Dto;
using Application.Streaming.Dto.Interfaces;
using LiteStreaming.AdministrativeApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AdministrativeApp.Controllers;

public class FlatControllerTest
{
    private readonly Mock<IFlatService> flatServiceMock;
    private readonly FlatController flatController;

    public FlatControllerTest()
    {
        flatServiceMock = new Mock<IFlatService>();
        flatController = new FlatController(flatServiceMock.Object)
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
        var flatsDto = MockFlat.Instance.GetDtoListFromFlatList(MockFlat.Instance.GetListFaker());
        flatServiceMock.Setup(service => service.FindAll()).Returns(flatsDto);

        // Act
        var result = flatController.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<FlatDto>>(viewResult.Model);
        Assert.Equal(flatsDto, model);
    }

    [Fact]
    public void Create_Returns_ViewResult()
    {
        // Act
        var result = flatController.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Save_ModelStateInvalid_Returns_CreateView()
    {
        // Arrange
        flatController.ModelState.AddModelError("test", "test error");

        // Act
        var result = flatController.Save(new FlatDto());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Create", viewResult.ViewName);
    }

    [Fact]
    public void Save_ValidDto_RedirectsToIndex()
    {
        // Arrange
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.UserId = dto.UsuarioId;

        // Act
        var result = flatController.Save(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        flatServiceMock.Verify(service => service.Create(dto), Times.Once);
    }

    [Fact]
    public void Save_ServiceThrowsArgumentException_Returns_CreateView_WithWarningAlert()
    {
        // Arrange
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.UserId = dto.UsuarioId;
        flatServiceMock.Setup(service => service.Create(It.IsAny<FlatDto>())).Throws(new ArgumentException("Invalid data."));

        // Act
        var result = flatController.Save(dto);

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
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.ControllerContext.HttpContext.Items["UserId"] = dto.UsuarioId;
        flatServiceMock.Setup(service => service.Create(It.IsAny<FlatDto>())).Throws(new Exception("Error"));

        // Act
        var result = flatController.Save(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Create", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao salvar os dados.", alert.Message);
    }

    [Fact]
    public void Update_ModelStateInvalid_Returns_EditView()
    {
        // Arrange
        flatController.ModelState.AddModelError("test", "test error");

        // Act
        var result = flatController.Update(new FlatDto());

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Edit", viewResult.ViewName);
    }

    [Fact]
    public void Update_ValidDto_RedirectsToIndex()
    {
        // Arrange
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.UserId = dto.UsuarioId;

        // Act
        var result = flatController.Update(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        flatServiceMock.Verify(service => service.Update(dto), Times.Once);
    }

    [Fact]
    public void Update_ServiceThrowsArgumentException_Returns_EditView_WithWarningAlert()
    {
        // Arrange
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.UserId = dto.UsuarioId;
        flatServiceMock.Setup(service => service.Update(It.IsAny<FlatDto>())).Throws(new ArgumentException("Invalid data."));

        // Act
        var result = flatController.Update(dto);

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
        var dto = MockFlat.Instance.GetFakerDto();
        flatController.UserId = null;
        flatServiceMock.Setup(service => service.Update(It.IsAny<FlatDto>())).Throws(new Exception("Error"));

        // Act
        var result = flatController.Update(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Edit", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao atualizar os dados deste usuário.", alert.Message);
    }

    [Fact]
    public void Edit_ValidId_Returns_ViewResult_With_Model()
    {
        // Arrange        
        var flatDto = MockFlat.Instance.GetFakerDto();
        var id = flatDto.Id;
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(flatDto);

        // Act
        var result = flatController.Edit(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<FlatDto>(viewResult.Model);
        Assert.Equal(flatDto, model);
    }

    [Fact]
    public void Edit_ServiceThrowsArgumentException_Returns_IndexView_WithWarningAlert()
    {
        // Arrange
        var id = Guid.NewGuid();
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new ArgumentException("Invalid user."));

        // Act
        var result = flatController.Edit(id);

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
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new Exception("Error"));

        // Act
        var result = flatController.Edit(id);

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
     
        var flatDto = MockFlat.Instance.GetFakerDto();
        var id = flatDto.Id;
        flatController.UserId = flatDto.UsuarioId;
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(flatDto);
        flatServiceMock.Setup(service => service.Delete(It.IsAny<FlatDto>())).Returns(true);
        
        // Act
        var result = flatController.Delete(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Sucesso", alert.Header);
        Assert.Equal("success", alert.Type);
        Assert.Equal("Usuário inativado.", alert.Message);
    }

    [Fact]
    public void Delete_ServiceThrowsArgumentException_Returns_IndexView_WithWarningAlert()
    {
        // Arrange
        var id = Guid.NewGuid();
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new ArgumentException("Invalid user."));

        // Act
        var result = flatController.Delete(id);

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
        var id = Guid.NewGuid();
        flatServiceMock.Setup(service => service.FindById(It.IsAny<Guid>())).Throws(new Exception("Error"));

        // Act
        var result = flatController.Delete(id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Index", viewResult.ViewName);
        var alert = Assert.IsType<AlertViewModel>(viewResult.ViewData["Alert"]);
        Assert.Equal("Erro", alert.Header);
        Assert.Equal("danger", alert.Type);
        Assert.Equal("Ocorreu um erro ao excluir os dados deste usuário.", alert.Message);
    }
}