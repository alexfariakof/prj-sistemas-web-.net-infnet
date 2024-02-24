using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Repository.Mapping.Account;
using __mock__;

namespace Repository.Mapping;
public class MerchantMapTest
{
    [Fact]
    public void EntityConfiguration_IsValid()
    {
        const int PROPERTY_COUNT = 7;

        // Arrange
        var options = new DbContextOptionsBuilder<MockRegisterContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase_MerchantMapTest")
            .Options;

        using (var context = new MockRegisterContext(options))
        {
            var builder = new ModelBuilder(new ConventionSet());
            var configuration = new MerchantMap();

            configuration.Configure(builder.Entity<Merchant>());

            var model = builder.Model;
            var entityType = model.FindEntityType(typeof(Merchant));
            var propsCount = entityType.GetNavigations().Count() + entityType.GetProperties().Count();

            // Act
            var idProperty = entityType.FindProperty("Id");
            var nameProperty = entityType.FindProperty("Name");
            var loginNavigation = entityType.FindNavigation(nameof(Merchant.Login));
            var emailProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Email));
            var passwordProperty = loginNavigation.TargetEntityType.FindProperty(nameof(Login.Password));
            var phoneNavigation = entityType.FindNavigation(nameof(Customer.Phone));
            var phoneProperty = phoneNavigation.TargetEntityType.FindProperty(nameof(Phone.Number));
            var cnpjProperty = entityType.FindProperty("CNPJ");

            // Assert
            Assert.NotNull(idProperty);
            Assert.NotNull(nameProperty);
            Assert.NotNull(loginNavigation);
            Assert.NotNull(emailProperty);
            Assert.NotNull(passwordProperty);
            Assert.NotNull(cnpjProperty);
            Assert.NotNull(phoneNavigation);
            Assert.NotNull(phoneProperty);

            Assert.True(idProperty.IsPrimaryKey());
            Assert.False(nameProperty.IsNullable);
            Assert.Equal(100, nameProperty.GetMaxLength());
            Assert.False(emailProperty.IsNullable);
            Assert.Equal(150, emailProperty.GetMaxLength());
            Assert.False(passwordProperty.IsNullable);
            Assert.Equal(255, passwordProperty.GetMaxLength());
            Assert.False(cnpjProperty.IsNullable);
            Assert.Equal(18, cnpjProperty.GetMaxLength());
            Assert.False(phoneProperty.IsNullable);
            Assert.Equal(50, phoneProperty.GetMaxLength());
            Assert.Equal(PROPERTY_COUNT, propsCount);
        }
    }
}
