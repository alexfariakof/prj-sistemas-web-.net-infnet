using __mock__;
using Domain.Account.ValueObject;

namespace Domain.Account;
public class LoginTest
{
    [Fact]
    public void Should_Success_With_Valid_Login()
    {
        // Arrange
        var mockLogin = MockLogin.GetFaker();

        // Act
        var login = new Login() 
        { 
            Email = mockLogin.Email,
            Password = mockLogin.Password,
        };

        // Assert
        Assert.Equal(mockLogin.Email, login.Email);
        Assert.NotEqual(mockLogin.Password, login.Password);
    }

    [Fact]
    public void Should_Throws_Erro_With_Invalid_Email()
    {
        // Arrange
        var mockLogin = new Login(); 

        // Act & Assert
        Assert.Throws<ArgumentException>(() => mockLogin.Email = "Email inválido!");
    }
            
    [Fact]
    public void Should_Throws_Erro_With_Long_Email()
    {
        // Arrange
        var mockLogin = MockLogin.GetFaker();

        // Act e Assert
        Assert.Throws<ArgumentException>(() => mockLogin.Email = new string('a', 257));
    }
}
