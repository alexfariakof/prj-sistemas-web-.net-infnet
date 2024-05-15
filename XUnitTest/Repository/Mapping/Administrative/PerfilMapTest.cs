using Domain.Administrative.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Repository.Mapping.Administrative;
public class PerfilMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 2; 

        // Arrange
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_PerfilMapTest")
            .Options;

        using (var context = new DbContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new PerfilMap();

            configuration.Configure(builder.Entity<Perfil>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Perfil));
            var propsCount = entityType?.GetNavigations().Count() + entityType?.GetProperties().Count();

            // Act
            var idProperty = entityType?.FindProperty("Id");
            var descriptionProperty = entityType?.FindProperty("Description");

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(descriptionProperty);

            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(descriptionProperty.IsNullable);
            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}
