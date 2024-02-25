using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Repository.Mapping.Account;
using __mock__;

namespace Repository.Mapping;
public class CustomerMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 10;
        // Arrange
        var options = new DbContextOptionsBuilder<MockRegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_CustomerMapTest")
            .Options;

        using (var context = new MockRegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new CustomerMap();

            configuration.Configure(builder.Entity<Customer>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Customer));
            var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

            // Act
            var idProperty = entityType.FindProperty("Id");
            var nameProperty = entityType.FindProperty("Name");
            var loginNavigation = entityType.FindNavigation(nameof(Customer.Login));
            var emailProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Email));
            var passwordProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Password));
            var birthProperty = entityType.FindProperty("Birth");
            var cpfProperty = entityType.FindProperty("CPF");
            var phoneNavigation = entityType.FindNavigation(nameof(Customer.Phone));
            var phoneProperty = phoneNavigation.TargetEntityType.FindProperty(nameof(Phone.Number));
            var playlistsNavigation = entityType.FindNavigation("Playlists");
            var addressesNavigation = entityType.FindNavigation("Addresses");
            
            // Assert
            // Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);
            Assert.NotNull(loginNavigation);
            Assert.NotNull(emailProperty);
            Assert.NotNull(passwordProperty);
            Assert.NotNull(birthProperty);
            Assert.NotNull(cpfProperty);
            Assert.NotNull(phoneNavigation);
            Assert.NotNull(phoneProperty);


            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(nameProperty.IsNullable);
            Assert.Equal(100, nameProperty.GetMaxLength());
            Assert.False(emailProperty.IsNullable);
            Assert.Equal(150, emailProperty.GetMaxLength());
            Assert.False(passwordProperty.IsNullable);
            Assert.Equal(255, passwordProperty.GetMaxLength());
            Assert.False(birthProperty.IsNullable);
            Assert.False(cpfProperty.IsNullable);
            Assert.Equal(14, cpfProperty.GetMaxLength());
            Assert.False(phoneProperty.IsNullable);
            Assert.Equal(50, phoneProperty.GetMaxLength());
            Assert.NotNull(playlistsNavigation);
            Assert.True(playlistsNavigation.IsCollection);
            Assert.NotNull(playlistsNavigation.ForeignKey.DeleteBehavior);
            Assert.NotNull(addressesNavigation);
            Assert.True(addressesNavigation.IsCollection);
            Assert.NotNull(addressesNavigation.ForeignKey.DeleteBehavior);

            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}