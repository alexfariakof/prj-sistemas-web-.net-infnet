using Domain.Account.ValueObject;
using Domain.Administrative.ValueObject;
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
        Assert.Equal(0, perfil?.Id);
        Assert.Null(perfil.Description);
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

    [Fact]
    public void Implicit_Conversion_From_UserType_Should_Create_PerfilUser()
    {
        // Arrange
        BasePerfil.UserType userType = BasePerfil.UserType.Merchant;

        // Act
        BasePerfil perfil = userType;

        // Assert
        Assert.IsType<PerfilUser>(perfil);
        Assert.Equal((int)userType, perfil.Id);
    }

    [Fact]
    public void Implicit_Conversion_From_Integer_Should_Create_PerfilUser()
    {
        // Arrange
        int value = (int)BasePerfil.UserType.Customer;

        // Act
        BasePerfil perfil = value;

        // Assert
        Assert.IsType<PerfilUser>(perfil);
        Assert.Equal(value, perfil.Id);
    }

    [Fact]
    public void Implicit_Conversion_To_UserType_Should_Return_Invalid_When_Null()
    {
        // Arrange
        BasePerfilClassTest? perfil = null;

        // Act
        BasePerfil.UserType userType = perfil;

        // Assert
        Assert.Equal(BasePerfil.UserType.Invalid, userType);
    }

    [Fact]
    public void Invalid_Type_From_Should_Throw_Exception()
    {
        // Arrange Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            BasePerfil.UserType userType = new Perfil(BasePerfil.UserType.Invalid);
        });

        Assert.Equal("Tipo de usuário inválido.", ex.Message);
    }
}
