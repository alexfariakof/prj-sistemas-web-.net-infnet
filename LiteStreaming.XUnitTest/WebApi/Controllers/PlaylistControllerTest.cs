﻿using Application.Streaming.Dto;
using Domain.Account.ValueObject;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApi.Controllers;
public class PlaylistControllerTest
{
    private Mock<IService<PlaylistDto>> mockPlaylistService;
    private PlaylistController controller;
    public PlaylistControllerTest()
    {
        mockPlaylistService = new Mock<IService<PlaylistDto>>();
        controller = new PlaylistController(mockPlaylistService.Object);
    }

    [Fact]
    public void FindAll_Returns_Ok_Result_When_List_Playlist_Found()
    {
        // Arrange        
        var userId = Guid.NewGuid();
        var playlistList = MockPlaylist.Instance.GetListFaker(2);
        var expectedPlaylistDtoList = MockPlaylist.Instance.GetDtoListFromPlaylistList(playlistList);
        mockPlaylistService.Setup(service => service.FindAll()).Returns(expectedPlaylistDtoList);
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.FindAll() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<PlaylistDto>>(result.Value);
        Assert.Equal(expectedPlaylistDtoList, result.Value);
    }

    [Fact]
    public void FindAll_Returns_NotFound_Result_When_List_Playlist_Not_Found()
    {
        // Arrange        
        var userId = Guid.NewGuid();
        mockPlaylistService.Setup(service => service.FindAll()).Returns(() => null);
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.FindAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public void FindAll_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var userId = Guid.NewGuid();
        mockPlaylistService.Setup(service => service.FindAll()).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.FindAll() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Playlist_Found()
    {
        // Arrange        
        var playlist = MockPlaylist.Instance.GetFaker();
        var expectedPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(playlist) ;
        mockPlaylistService.Setup(service => service.FindById(playlist.Id)).Returns(expectedPlaylistDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(playlist.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistDto>(result.Value);
        Assert.Equal(expectedPlaylistDto, result.Value);
    }

    [Fact]
    public void FindById_Returns_NotFound_Result_When_Playlist_Not_Found()
    {
        // Arrange        
        var playlistId = Guid.NewGuid();
        mockPlaylistService.Setup(service => service.FindById(playlistId)).Returns(() => null);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(playlistId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void FindById_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var playlistId = Guid.NewGuid();
        mockPlaylistService.Setup(service => service.FindById(playlistId)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(playlistId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var playlist = MockPlaylist.Instance.GetFaker();
        var validPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(playlist);
        mockPlaylistService.Setup(service => service.Create(validPlaylistDto)).Returns(validPlaylistDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);

        // Act
        var result = controller.Create(validPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistDto>(result.Value);
        Assert.Equal(validPlaylistDto, result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange       
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Create(It.IsAny<PlaylistDto>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var invalidPlaylistDto = new PlaylistDto(); // Invalid DTO to trigger exception in the service
        mockPlaylistService.Setup(service => service.Create(invalidPlaylistDto)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);

        // Act
        var result = controller.Create(invalidPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(MockPlaylist.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Update(validPlaylistDto)).Returns(validPlaylistDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);
        // Act
        var result = controller.Update(validPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistDto>(result.Value);
        Assert.Equal(validPlaylistDto, result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Update(It.IsAny<PlaylistDto>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var validPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(MockPlaylist.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Update(validPlaylistDto)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);

        // Act
        var result = controller.Update(validPlaylistDto);

        // Assert
        Assert.NotNull(result);
        var value = Assert.IsType<BadRequestObjectResult>(result).Value;
        Assert.Equal("BadRequest_Erro_Message", value);
    }

    [Fact]
    public void Delete_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var mockPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(MockPlaylist.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Delete(It.IsAny<PlaylistDto>())).Returns(true);
        mockPlaylistService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(mockPlaylistDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);

        // Act
        var result = controller.Delete(mockPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.True((bool?)result.Value);
        mockPlaylistService.Verify(b => b.Delete(It.IsAny<PlaylistDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        PlaylistDto? nullPlaylistDto = null;
        var result = controller.Delete(nullPlaylistDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockPlaylistService.Verify(b => b.Delete(It.IsAny<PlaylistDto>()), Times.Never);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var mockPlaylistDto = MockPlaylist.Instance.GetDtoFromPlaylist(MockPlaylist.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Delete(It.IsAny<PlaylistDto>())).Throws(new Exception("BadRequest_Erro_Message"));
        mockPlaylistService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(mockPlaylistDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Admin);

        // Act
        var result = controller.Delete(mockPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }
}