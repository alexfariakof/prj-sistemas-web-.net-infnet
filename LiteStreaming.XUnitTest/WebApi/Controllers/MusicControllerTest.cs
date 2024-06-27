using Application.Streaming.Dto;
using LiteStreaming.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApi.Controllers;
public class MusicControllerTest
{
    private Mock<IService<MusicDto>> mockMusicService;
    private MusicController controller;

    public MusicControllerTest()
    {
        mockMusicService = new Mock<IService<MusicDto>>();
        controller = new MusicController(mockMusicService.Object);
    }

    [Fact]
    public void Search_Returns_Ok_Result_With_Valid_Search_Param()
    {
        // Arrange
        var userId = Guid.NewGuid();
        string searchParam = "a";
        var musicList = MockMusic.Instance.GetListFaker(10);
        var expectedMusicDtoList = MockMusic.Instance.GetDtoListFromMusicList(musicList.Where(m => m.Name.ToLower().Contains(searchParam.ToLower())).ToList());

        mockMusicService.Setup(service => service.FindAll(It.IsAny<Guid>())).Returns(expectedMusicDtoList);
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.Serach(searchParam) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var musicDtoArray = Assert.IsAssignableFrom<IEnumerable<MusicDto>>(result.Value);
        Assert.Equal(expectedMusicDtoList.Count, musicDtoArray.Count());
        mockMusicService.Verify(x => x.FindAll(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void FindAll_Returns_Ok_Result_When_List_Music_Found()
    {
        // Arrange        
        var userId = Guid.NewGuid();
        var musicList = MockMusic.Instance.GetListFaker(2);
        var expectedMusicDtoList = MockMusic.Instance.GetDtoListFromMusicList(musicList);
        mockMusicService.Setup(service => service.FindAll(It.IsAny<Guid>())).Returns(expectedMusicDtoList);
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.FindAll() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<MusicDto>>(result.Value);
        Assert.Equal(expectedMusicDtoList, result.Value);
    }

    [Fact]
    public void FindAll_Returns_NotFound_Result_When_List_Music_Not_Found()
    {
        // Arrange        
        var userId = Guid.NewGuid();
        mockMusicService.Setup(service => service.FindAll(userId)).Returns(() => null);
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
        mockMusicService.Setup(service => service.FindAll(It.IsAny<Guid>())).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(userId, controller);

        // Act
        var result = controller.FindAll() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void FindById_Returns_Ok_Result_When_Music_Found()
    {
        // Arrange        
        var music = MockMusic.Instance.GetFaker();
        var expectedMusicDto = MockMusic.Instance.GetDtoFromMusic(music) ;
        mockMusicService.Setup(service => service.FindById(music.Id)).Returns(expectedMusicDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(music.Id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MusicDto>(result.Value);
        Assert.Equal(expectedMusicDto, result.Value);
    }

    [Fact]
    public void FindById_Returns_NotFound_Result_When_Music_Not_Found()
    {
        // Arrange        
        var musicId = Guid.NewGuid();
        mockMusicService.Setup(service => service.FindById(musicId)).Returns(() => null);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(musicId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var music = MockMusic.Instance.GetFaker();
        var validMusicDto = MockMusic.Instance.GetDtoFromMusic(music);
        mockMusicService.Setup(service => service.Create(validMusicDto)).Returns(validMusicDto);

        // Act
        var result = controller.Create(validMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MusicDto>(result.Value);
        Assert.Equal(validMusicDto, result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange       
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Create(It.IsAny<MusicDto>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Update_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var validMusicDto = MockMusic.Instance.GetDtoFromMusic(MockMusic.Instance.GetFaker());
        mockMusicService.Setup(service => service.Update(validMusicDto)).Returns(validMusicDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);
        // Act
        var result = controller.Update(validMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.IsType<MusicDto>(result.Value);
        Assert.Equal(validMusicDto, result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        var result = controller.Update(It.IsAny<MusicDto>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void Delete_Returns_Ok_Result_When_ModelState_Is_Valid()
    {
        // Arrange        
        var mockMusicDto = MockMusic.Instance.GetDtoFromMusic(MockMusic.Instance.GetFaker());
        mockMusicService.Setup(service => service.Delete(It.IsAny<MusicDto>())).Returns(true);
        mockMusicService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(mockMusicDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.Delete(mockMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.True((bool?)result.Value);
        mockMusicService.Verify(b => b.Delete(It.IsAny<MusicDto>()), Times.Once);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_When_ModelState_Is_Invalid()
    {
        // Arrange
        Usings.SetupBearerToken(Guid.NewGuid(), controller);
        controller.ModelState.AddModelError("errorKey", "ErrorMessage");

        // Act
        MusicDto? nullMusicDto = null;
        var result = controller.Delete(nullMusicDto);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        mockMusicService.Verify(b => b.Delete(It.IsAny<MusicDto>()), Times.Never);
    }

    [Fact]
    public void FindById_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var musicId = Guid.NewGuid();
        mockMusicService.Setup(service => service.FindById(musicId)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.FindById(musicId) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Create_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var invalidMusicDto = new MusicDto(); // Invalid DTO to trigger exception in the service
        mockMusicService.Setup(service => service.Create(invalidMusicDto)).Throws(new Exception("BadRequest_Erro_Message"));

        // Act
        var result = controller.Create(invalidMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Update_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var validMusicDto = MockMusic.Instance.GetDtoFromMusic(MockMusic.Instance.GetFaker());
        mockMusicService.Setup(service => service.Update(validMusicDto)).Throws(new Exception("BadRequest_Erro_Message"));
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.Update(validMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }

    [Fact]
    public void Delete_Returns_BadRequest_Result_On_Exception()
    {
        // Arrange        
        var mockMusicDto = MockMusic.Instance.GetDtoFromMusic(MockMusic.Instance.GetFaker());
        mockMusicService.Setup(service => service.Delete(It.IsAny<MusicDto>())).Throws(new Exception("BadRequest_Erro_Message"));
        mockMusicService.Setup(service => service.FindById(It.IsAny<Guid>())).Returns(mockMusicDto);
        Usings.SetupBearerToken(Guid.NewGuid(), controller);

        // Act
        var result = controller.Delete(mockMusicDto) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("BadRequest_Erro_Message", result.Value);
    }
}