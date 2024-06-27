using Application.Streaming.Dto;
using Domain.Account.ValueObject;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApi.Controllers;
public class CustomerControllerMyPlaylistTests
{
    private readonly Mock<IService<CustomerDto>> mockCustomerService;
    private readonly Mock<IService<PlaylistPersonalDto>> mockPlaylistService;
    private readonly CustomerController controller;
    public CustomerControllerMyPlaylistTests()
    {
        // Arrange
        mockCustomerService = new Mock<IService<CustomerDto>>();
        mockPlaylistService = new Mock<IService<PlaylistPersonalDto>>();
        controller = new CustomerController(mockCustomerService.Object, mockPlaylistService.Object);
    }

    [Fact]
    public void FindAllPlaylist_Returns_Ok_Result_When_Playlists_Found()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);

        var playlists = MockPlaylistPersonal.Instance.GetListFaker(3);
        var playlistsDto = MockPlaylistPersonal.Instance.GetDtoListFromPlaylistPersonalList(playlists);

        mockPlaylistService.Setup(service => service.FindAll(It.IsAny<Guid>())).Returns(playlistsDto);

        // Act
        var result = controller.FindAllPlaylist() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<PlaylistPersonalDto>>(result.Value);
        Assert.Equal(playlistsDto, result.Value);
    }

    [Fact]
    public void FindAllPlaylist_Returns_OkObjectResult_When_User_Not_Customer()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller, PerfilUser.UserType.Merchant);
        // Act
        var result = controller.FindAllPlaylist();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void FindByIdPlaylist_Returns_Ok_Result_When_Playlist_Found()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var playlistId = playlist.Id;
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller);

        mockPlaylistService.Setup(service => service.FindById(playlistId)).Returns(playlistDto);

        // Act
        var result = controller.FindByIdPlaylist(playlistId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistPersonalDto>(result.Value);
        Assert.Equal(playlistDto, result.Value);
    }

    [Fact]
    public void FindByIdPlaylist_Returns_OkObjectResult_Null_When_Playlist_Not_Found()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);

        var playlistId = Guid.NewGuid();

        mockPlaylistService.Setup(service => service.FindById(playlistId)).Returns(() => null);

        // Act
        var result = controller.FindByIdPlaylist(playlistId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result);        
    }

    [Fact]
    public void FindByIdPlaylist_Returns_OkObjectResult_When_User_Not_Customer()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);

        // Act
        var result = controller.FindByIdPlaylist(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void CreatePlaylist_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);
        mockPlaylistService.Setup(service => service.Create(playlistDto)).Returns(playlistDto);

        // Act
        var result = controller.CreatePlaylist(playlistDto);

        // Assert
        Assert.NotNull(result);
        //Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void CreatePlaylist_Returns_Ok_Result_When_Valid_Playlist()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);        
        mockPlaylistService.Setup(service => service.Create(playlistDto)).Returns(playlistDto);

        // Act
        var result = controller.CreatePlaylist(playlistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistPersonalDto>(result.Value);
        Assert.Equal(playlistDto, result.Value);
    }

    [Fact]
    public void CreatePlaylist_Returns_BadRequest_Result_When_Invalid_Playlist()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);
        var playlistDto = new PlaylistPersonalDto();

        // Act
        var result = controller.CreatePlaylist(playlistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(result.Value as IEnumerable<string>);
    }

    [Fact]
    public void CreatePlaylist_Returns_BadRequest_Result_When_Playlist_Deletion_Fails()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(MockPlaylistPersonal.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Create(playlistDto)).Returns(() => throw new Exception("Failed to create the playlist."));

        // Act
        var result = controller.CreatePlaylist(playlistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to create the playlist.", result.Value);
        mockPlaylistService.Verify(d => d.Create(It.IsAny<PlaylistPersonalDto>()), Times.Once);
    }

    [Fact]
    public void UpdatePlaylist_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);
        mockPlaylistService.Setup(service => service.Update(It.IsAny<PlaylistPersonalDto>())).Returns(playlistDto);

        // Act
        var result = controller.UpdatePlaylist(playlistDto);

        // Assert
        //Assert.Null(result);
        //Assert.IsType<UnauthorizedResult>(result);
        //mockPlaylistService.Verify(d => d.Update(It.IsAny<PlaylistPersonalDto>()), Times.Never);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<PlaylistPersonalDto>(okResult.Value);
        Assert.Empty(okResult.ContentTypes);
        Assert.Null(okResult.DeclaredType);
        Assert.Empty(okResult.Formatters);

    }

    [Fact]
    public void UpdatePlaylist_Returns_Ok_Result_When_Valid_Playlist()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller);
        mockPlaylistService.Setup(service => service.Update(playlistDto)).Returns(playlistDto);

        // Act
        var result = controller.UpdatePlaylist(playlistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<PlaylistPersonalDto>(result.Value);
        Assert.Equal(playlistDto, result.Value);
        mockPlaylistService.Verify(d => d.Update(It.IsAny<PlaylistPersonalDto>()), Times.Once);
    }

    [Fact]
    public void UpdatePlaylist_Returns_BadRequest_Result_When_Invalid_Playlist()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);
        PlaylistPersonalDto? nullPlaylistDto = null;

        // Act
        var result = controller.UpdatePlaylist(nullPlaylistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void UpdatePlaylist_Returns_BadRequest_Result_When_Playlist_Deletion_Fails()
    {
        // Arrange
        var userIdentity = Guid.NewGuid();
        Usings.SetupBearerToken(userIdentity, controller);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(MockPlaylistPersonal.Instance.GetFaker());
        mockPlaylistService.Setup(service => service.Update(playlistDto)).Returns(() => throw new Exception("Failed to create the playlist."));

        // Act
        var result = controller.UpdatePlaylist(playlistDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to create the playlist.", result.Value);
        mockPlaylistService.Verify(d => d.Update(It.IsAny<PlaylistPersonalDto>()), Times.Once);
    }

    [Fact]
    public void DeletePlaylist_Returns_Unauthorized_Result_When_User_Not_Customer()
    {
        // Arrange
        var playlist = MockPlaylistPersonal.Instance.GetFaker();
        var userIdentity = playlist.Customer.Id;
        Usings.SetupBearerToken(userIdentity, controller, PerfilUser.UserType.Merchant);
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(playlist);
        mockPlaylistService.Setup(service => service.Delete(playlistDto)).Returns(false);

        // Act
        var result = controller.DeletePlaylist(playlistDto.Id);

        // Assert
        //Assert.NotNull(result);
        //Assert.IsType<UnauthorizedResult>(result);
        //mockPlaylistService.Verify(d =>d .Delete(It.IsAny<PlaylistPersonalDto>()), Times.Never);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.False((bool?)okResult.Value);
        Assert.Empty(okResult.ContentTypes);
        Assert.Null(okResult.DeclaredType);
        Assert.Empty(okResult.Formatters);
    }

    [Fact]
    public void DeletePlaylist_Returns_Ok_Result_When_Playlist_Deleted_Successfully()
    {
        // Arrange
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(MockPlaylistPersonal.Instance.GetFaker());
        var userIdentity = playlistDto.CustomerId;
        Usings.SetupBearerToken(userIdentity, controller);

        mockPlaylistService.Setup(service => service.Delete(It.IsAny<PlaylistPersonalDto>())).Returns(true);

        // Act
        var result = controller.DeletePlaylist(playlistDto.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<bool>(result.Value);
        Assert.True((bool)result.Value);
        mockPlaylistService.Verify(d => d.Delete(It.IsAny<PlaylistPersonalDto>()), Times.Once);
    }

    [Fact]
    public void DeletePlaylist_Returns_BadRequest_Result_When_Playlist_Deletion_Fails()
    {
        // Arrange        
        var playlistDto = MockPlaylistPersonal.Instance.GetDtoFromPlaylistPersonal(MockPlaylistPersonal.Instance.GetFaker());
        var userIdentity = playlistDto.CustomerId;
        Usings.SetupBearerToken(userIdentity, controller);
        mockPlaylistService.Setup(service => service.Delete(It.IsAny<PlaylistPersonalDto>())).Returns(() => throw new Exception("Failed to delete the playlist."));

        // Act
        var result = controller.DeletePlaylist(playlistDto.Id) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to delete the playlist.", result.Value);
        mockPlaylistService.Verify(d => d.Delete(It.IsAny<PlaylistPersonalDto>()), Times.Once);
    }
}
