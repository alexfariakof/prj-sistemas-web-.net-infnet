using Domain.Core.ValueObject;

namespace Domain.Core;

public class BasePerfilTest
{
    private record BasePerfilClassTest : BasePerfil
    {
        public BasePerfilClassTest() : base() { }
        public BasePerfilClassTest(BasePerfil.UserType type) : base(type) { }
    }

    [Fact]
    public void Default_Constructor_Should_Set_Default_Values()
    {
        // Arrange & Act
        var perfil = new BasePerfilClassTest();

        // Assert
        Assert.Equal(0, perfil.Id);
        Assert.Equal("Invalid", perfil.Description);
    }

    [Fact]
    public void Constructor_With_UserType_Should_Set_Correct_Values()
    {
        // Arrange & Act
        var adminPerfil = new BasePerfilClassTest(BasePerfil.UserType.Admin);
        var customerPerfil = new BasePerfilClassTest(BasePerfil.UserType.Customer);

        // Assert
        Assert.Equal((int)BasePerfil.UserType.Admin, adminPerfil.Id);
        Assert.Equal("Administrador", adminPerfil.Description);

        Assert.Equal((int)BasePerfil.UserType.Customer, customerPerfil.Id);
        Assert.Equal("Customer", customerPerfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_To_UserType_Should_Work_Correctly()
    {
        // Arrange
        var perfil = new BasePerfilClassTest(BasePerfil.UserType.Merchant);

        // Act
        BasePerfil.UserType userType = perfil;

        // Assert
        Assert.Equal(perfil.Id, (int)userType);
    }

    [Fact]
    public void Operator_Equal_And_NotEqual_Should_Work_Correctly()
    {
        // Arrange
        var perfil1 = new BasePerfilClassTest(BasePerfil.UserType.Customer);
        var perfil2 = new BasePerfilClassTest(BasePerfil.UserType.Merchant);
        var perfil3 = new BasePerfilClassTest(BasePerfil.UserType.Customer);

        // Act & Assert
        Assert.True(perfil1 != BasePerfil.UserType.Merchant);
        Assert.True(perfil2 != BasePerfil.UserType.Customer);
        Assert.True(perfil1 == BasePerfil.UserType.Customer);
        Assert.True(perfil1 == perfil3);
        Assert.False(perfil1 != perfil3);
    }
}
