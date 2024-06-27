using LiteStreaming.AdministrativeApp.Controllers;
using LiteStreaming.AdministrativeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdministrativeApp.Controllers;
public class HomeControllerTest
{
    private readonly Mock<ILogger<HomeController>> loggerMock;
    private readonly HomeController homeController;

    public HomeControllerTest()
    {
        loggerMock = new Mock<ILogger<HomeController>>();
        homeController = new HomeController(loggerMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = Usings.MockHttpContext(),
            }
        };
    }

    [Fact]
    public void Index_Returns_ViewResult()
    {
        // Act
        var result = homeController.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Privacy_Returns_ViewResult()
    {
        // Act
        var result = homeController.Privacy();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewName);
    }

    [Fact]
    public void Error_Returns_ViewResult_With_ErrorViewModel()
    {
        // Arrange
        homeController.ControllerContext.HttpContext.TraceIdentifier = "test_trace_id";

        // Act
        var result = homeController.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.Equal("test_trace_id", model.RequestId);
    }

  
}
