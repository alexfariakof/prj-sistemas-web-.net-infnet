using Application.Shared.Dto;
using Application.Authentication;
using AutoMapper;
using Domain.Account.Agreggates;
using Moq;
using Repository.Interfaces;
using System.Linq.Expressions;
using LiteStreaming.Cryptography;
using Microsoft.Extensions.Options;

namespace Application.Account;
public class UserServiceTest
{
    private Mock<IMapper> mapperMock;
    private Mock<IRepository<User>> userRepositoryMock;
    private readonly UserService userService;
    private readonly List<User> mockUserList = MockCustomer.Instance.GetListFaker(3).Select(u => u.User).ToList();
    private readonly Mock<ICrypto> cryptoMock;
    public UserServiceTest()
    {
        var options = Options.Create(new TokenOptions
        {
            Issuer = "testIssuer",
            Audience = "testAudience",
            Seconds = 3600
        });

        var signingConfigurations = new SigningConfigurations(options);
        mapperMock = new Mock<IMapper>();
        cryptoMock = new Mock<ICrypto>();
        userRepositoryMock = Usings.MockRepositorio(mockUserList);
        userService = new UserService(mapperMock.Object, userRepositoryMock.Object, signingConfigurations);
    }

    [Fact]
    public void Authentication_With_Valid_Credentials_Should_Return_AuthenticationDto()
    {
        // Arrange
        var mockUser = mockUserList.First();
        mockUser.Login.Password = "validPassword";
        var loginDto = new LoginDto { Email = mockUser.Login.Email, Password = "validPassword" };
        
        userRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(mockUserList.Where(u => u.Login.Email.Equals(mockUser.Login.Email)));
        cryptoMock.Setup(crypto => crypto.Encrypt(It.IsAny<string>())).Returns(mockUser.Login.Password);

        // Act
        var result = userService.Authentication(loginDto);

        // Assert
        userRepositoryMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(result.AccessToken);
    }

    [Fact]
    public void Authentication_With_Invalid_Credentials_Should_Throw_Exception()
    {
        // Arrange
        var mockUser = mockUserList.First();
        var loginDto = new LoginDto { Email = "invalid.email@example.com", Password = "invalidPassword" };
        userRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(mockUserList.Where(u => u.Login.Email.Equals(mockUser.Login.Email)));
        cryptoMock.Setup(crypto => crypto.Encrypt(It.IsAny<string>())).Returns(mockUser.Login.Password);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => userService.Authentication(loginDto));
        Assert.Equal("Usuário Inválido!", exception.Message);
        userRepositoryMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
    }
}