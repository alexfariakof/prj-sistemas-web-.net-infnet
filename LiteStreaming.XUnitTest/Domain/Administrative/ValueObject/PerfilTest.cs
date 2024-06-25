using Domain.Administrative.ValueObject;
using static Domain.Core.ValueObject.BasePerfil;

namespace Domain.Administrative;

public class PerfilTest
{
    [Fact]
    public void Perfil_Default_Constructor_Should_Set_Default_Values()
    {
        // Arrange & Act
        var perfil = new Perfil();

        // Assert
        Assert.Equal(0, perfil?.Id);
        Assert.Null(perfil.Description);
    }

    [Fact]
    public void Perfil_Constructor_With_UserType_Should_Set_Correct_Values()
    {
        // Arrange & Act
        var customerPerfil = new Perfil(UserType.Customer);
        var merchantPerfil = new Perfil(UserType.Merchant);

        // Assert
        Assert.Equal((int)UserType.Customer, customerPerfil.Id);
        Assert.Equal("Customer", customerPerfil.Description);

        Assert.Equal((int)UserType.Merchant, merchantPerfil.Id);
        Assert.Equal("Merchant", merchantPerfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_From_UserType_Should_Work_Correctly()
    {
        // Arrange
        UserType userType = UserType.Customer;

        // Act
        Perfil perfil = userType;

        // Assert
        Assert.Equal((int)userType, perfil.Id);
        Assert.Equal("Customer", perfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_To_UserType_Should_Work_Correctly()
    {
        // Arrange
        var perfil = new Perfil(UserType.Merchant);

        // Act
        UserType userType = perfil;

        // Assert
        Assert.Equal(perfil.Id, (int)userType);
    }

    [Fact]
    public void Implicit_Conversion_From_Integer_Should_Work_Correctly()
    {
        // Arrange
        int value = 1;

        // Act
        Perfil perfil = value;

        // Assert
        Assert.Equal(value, perfil.Id);
    }

    [Fact]
    public void Operator_Equal_And_NotEqual_Should_Work_Correctly()
    {
        // Arrange
        var perfil1 = new Perfil(UserType.Customer);
        var perfil2 = new Perfil(UserType.Merchant);
        var perfil3 = new Perfil(UserType.Customer);

        // Act & Assert
        Assert.True(perfil1 != UserType.Merchant);
        Assert.True(perfil2 != UserType.Customer);
        Assert.True(perfil1 == UserType.Customer);
        Assert.True(perfil1 == perfil3);
        Assert.False(perfil1 != perfil3);
    }
}
