using Domain.Administrative.Agreggates;
using Domain.Administrative.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class RegisterContextAdministravtiveTest
{
    [Fact]
    public void Should_Have_DbSets_RegisterContextAdministravtive()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministravtive>().UseInMemoryDatabase(databaseName: "RegisterContextAdministravtive_TestDatabase_Options").Options;

        // Act
        using (var context = new RegisterContextAdministravtive(options))
        {
            // Assert
            Assert.NotNull(context.Admin);
            Assert.NotNull(context.Perfil);
        }
    }

    [Fact]
    public void Should_Apply_Configurations_RegisterContextAdministravtive()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministravtive>().UseInMemoryDatabase(databaseName: "InMemory_DataBase_RegisterContextAdministravtive").Options;

        // Act
        using (var context = new RegisterContextAdministravtive(options))
        {
            // Assert
            var model = context.Model;
            Assert.True(model.FindEntityType(typeof(AdministrativeAccount)) != null);
            Assert.True(model.FindEntityType(typeof(Perfil)) != null);
        }
    }
}