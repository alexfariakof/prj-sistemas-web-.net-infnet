using Microsoft.IdentityModel.Tokens;

namespace Application.Security;
public class SigningConfigurationsTest
{
    [Fact]
    public void SigningConfigurations_Should_Initialize_Correctly()
    {
        // Arrange & Act
        var signingConfigurations = SigningConfigurations.Instance;

        // Assert
        Assert.NotNull(signingConfigurations.Key);
        Assert.NotNull(signingConfigurations.SigningCredentials);
    }

    [Fact]
    public void Key_Should_Be_RSA_SecurityKey()
    {
        // Arrange
        var signingConfigurations = SigningConfigurations.Instance;

        // Assert
        Assert.IsType<RsaSecurityKey>(signingConfigurations.Key);
    }

    [Fact]
    public void SigningCredentials_Should_Be_Correct_Algorithm()
    {
        // Arrange
        var signingConfigurations = SigningConfigurations.Instance;

        // Assert
        Assert.NotNull(signingConfigurations.SigningCredentials.Algorithm);
        Assert.Equal(SecurityAlgorithms.RsaSha256Signature, signingConfigurations.SigningCredentials.Algorithm);
    }
}
