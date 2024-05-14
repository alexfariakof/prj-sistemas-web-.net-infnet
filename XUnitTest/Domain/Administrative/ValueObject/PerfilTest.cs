using Domain.Administrative.ValueObject;

namespace Domain.Administrative;
public class PerfilTest
{
    [Fact]
    public void Perfil_Default_Constructor_Should_Set_Default_Values()
    {
        // Arrange & Act
        var perfil = new Perfil();

        // Assert
        Assert.Equal(0, perfil.Id);
        Assert.Null(perfil.Description);
    }

    [Fact]
    public void Perfil_Constructor_With_PerfilType_Should_Set_Correct_Values()
    {
        // Arrange & Act
        var adminPerfil = new Perfil(Perfil.PerfilType.Admin);
        var normalPerfil = new Perfil(Perfil.PerfilType.Normal);

        // Assert
        Assert.Equal((int)Perfil.PerfilType.Admin, adminPerfil.Id);
        Assert.Equal("Administrador", adminPerfil.Description);

        Assert.Equal((int)Perfil.PerfilType.Normal, normalPerfil.Id);
        Assert.Equal("Normal", normalPerfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_From_PerfilType_Should_Work_Correctly()
    {
        // Arrange
        Perfil.PerfilType perfilType = Perfil.PerfilType.Admin;

        // Act
        Perfil perfil = perfilType;

        // Assert
        Assert.Equal((int)perfilType, perfil.Id);
        Assert.Equal("Administrador", perfil.Description);
    }

    [Fact]
    public void Implicit_Conversion_To_PerfilType_Should_Work_Correctly()
    {
        // Arrange
        var perfil = new Perfil(Perfil.PerfilType.Normal);

        // Act
        Perfil.PerfilType perfilType = perfil;

        // Assert
        Assert.Equal(perfil.Id, (int)perfilType);
    }

    [Fact]
    public void Implicit_Conversion_From_Integer_Should_Work_Correctly()
    {
        // Arrange
        int value = 1;

        // Act
        Perfil perfil = (Perfil.PerfilType)value;

        // Assert
        Assert.Equal(value, perfil.Id);
    }

    [Fact]
    public void Operator_Equal_And_NotEqual_Should_Work_Correctly()
    {
        // Arrange
        var perfil1 = new Perfil(Perfil.PerfilType.Admin);
        var perfil2 = new Perfil(Perfil.PerfilType.Normal);
        var perfil3 = new Perfil(Perfil.PerfilType.Admin);

        // Act & Assert
        Assert.True(perfil1 != Perfil.PerfilType.Normal);
        Assert.True(perfil2 != Perfil.PerfilType.Admin);
        Assert.True(perfil1 == Perfil.PerfilType.Admin);
        Assert.True(perfil1 == perfil3);
        Assert.False(perfil1 != perfil3);
    }
}
