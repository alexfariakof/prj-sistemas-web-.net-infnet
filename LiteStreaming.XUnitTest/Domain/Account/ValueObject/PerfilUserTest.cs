using Domain.Account.ValueObject;

namespace Domain.Account;
public class PerfilUserTest
{
    [Fact]
    public void Perfil_Default_Constructor_Should_Set_Default_Values()
    {
        // Arrange & Act
        var perfil = new PerfilUser();

        // Assert
        Assert.Equal(0, perfil.Id);
        Assert.NotNull(perfil.Description);
    }

    [Fact]
    public void Perfil_Constructor_With_PerfilType_Should_Set_Correct_Values()
    {
        // Arrange & Act
        var customerPerfil = new PerfilUser(PerfilUser.UserType.Customer);
        var merchantPerfil = new PerfilUser(PerfilUser.UserType.Merchant);

        // Assert
        Assert.Equal((int)PerfilUser.UserType.Customer, customerPerfil.Id);
        Assert.Equal("Customer", customerPerfil.Description);

        Assert.Equal((int)PerfilUser.UserType.Merchant, merchantPerfil.Id);
        Assert.Equal("Merchant", merchantPerfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_From_PerfilType_Should_Work_Correctly()
    {
        // Arrange
        PerfilUser.UserType userType = PerfilUser.UserType.Customer;

        // Act
        PerfilUser perfil = userType;

        // Assert
        Assert.Equal((int)userType, perfil.Id);
        Assert.Equal("Customer", perfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_To_PerfilType_Should_Work_Correctly()
    {
        // Arrange
        var perfil = new PerfilUser(PerfilUser.UserType.Merchant);

        // Act
        PerfilUser.UserType userType = perfil;

        // Assert
        Assert.Equal(perfil.Id, (int)userType);
    }

    [Fact]
    public void Implicit_Conversion_From_Integer_Should_Work_Correctly()
    {
        // Arrange
        int value = 1;

        // Act
        PerfilUser perfil = (PerfilUser.UserType)value;

        // Assert
        Assert.Equal(value, perfil.Id);
    }

    [Fact]
    public void Operator_Equal_And_NotEqual_Should_Work_Correctly()
    {
        // Arrange
        var perfil1 = new PerfilUser(PerfilUser.UserType.Customer);
        var perfil2 = new PerfilUser(PerfilUser.UserType.Merchant);
        var perfil3 = new PerfilUser(PerfilUser.UserType.Customer);

        // Act & Assert
        Assert.True(perfil1 != PerfilUser.UserType.Merchant);
        Assert.True(perfil2 != PerfilUser.UserType.Customer);
        Assert.True(perfil1 == PerfilUser.UserType.Customer);
        Assert.True(perfil1 == perfil3);
        Assert.False(perfil1 != perfil3);
    }
}
