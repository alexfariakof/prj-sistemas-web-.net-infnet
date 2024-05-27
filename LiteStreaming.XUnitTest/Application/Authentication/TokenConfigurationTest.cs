using Microsoft.Extensions.Options;

namespace Application.Authentication;
public class TokenConfigurationTest
{
    [Fact]
    public void Properties_Should_Be_Set_Correctly()
    {
        // Arrange
        var options = Options.Create(new TokenOptions
        {
            Issuer = "testIssuer",
            Audience = "testAudience",
            Seconds = 3600
        });
        var tokenConfiguration = new TokenConfiguration(options);

        // Act
        tokenConfiguration.Audience = "TesteAudience";
        tokenConfiguration.Issuer = "TesteIssuer";
        tokenConfiguration.Seconds = 3600; // 1 hour

        // Assert
        Assert.Equal("TesteAudience", tokenConfiguration.Audience);
        Assert.Equal("TesteIssuer", tokenConfiguration.Issuer);
        Assert.Equal(3600, tokenConfiguration.Seconds);
    }
}
