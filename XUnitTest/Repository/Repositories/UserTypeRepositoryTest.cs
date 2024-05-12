using Domain.Account.ValueObject;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Repository.Repositories;
public class UserTypeRepositoryTest
{
    private Mock<RegisterContext> contextMock;

    public UserTypeRepositoryTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_UserTypeRepositoryTest")
            .Options;

        contextMock = new Mock<RegisterContext>(options);
    }

    [Fact]
    public void GetById_Should_Return_UserType_With_Correct_Id()
    {
        // Arrange
        contextMock.Object.Add(Usings.MockDataSetUserType().Object);
        var repository = new UserTypeRepository(contextMock.Object);
        var mockUserType = new PerfilUser(PerfilUser.UserlType.Customer);
        contextMock.Setup<PerfilUser>(c => c.Set<PerfilUser>().Find(It.IsAny<int>())).Returns((PerfilUser)mockUserType);        

        // Act
        var result = repository.GetById(mockUserType.Id);

        // Assert
        Assert.Equal(mockUserType, result);
    }

}