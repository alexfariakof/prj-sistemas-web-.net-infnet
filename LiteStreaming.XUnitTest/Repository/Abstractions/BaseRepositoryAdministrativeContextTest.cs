using __mock__.Admin;
using Domain.Administrative.Agreggates;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Repository.Abstractions;
public class BaseRepositoryAdministrativeContextTest
{
    public class TestRepository : BaseRepository<AdministrativeAccount>
    {
        public TestRepository(RegisterContextAdministrative context) : base(context) { }
    }


    private Mock<RegisterContextAdministrative> contextMock;
    public BaseRepositoryAdministrativeContextTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
        contextMock = new Mock<RegisterContextAdministrative>(options);
    }

    [Fact]
    public void Save_Should_Add_Entity_And_SaveChanges()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entity = new AdministrativeAccount();

        // Act
        repository.Save(entity);

        // Assert
        contextMock.Verify(c => c.Add(entity), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Update_Should_Update_Entity_And_SaveChanges()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entity = new AdministrativeAccount();

        // Act
        repository.Update(entity);

        // Assert
        contextMock.Verify(c => c.Update(entity), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void Delete_Should_Remove_Entity_And_SaveChanges()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entity = new AdministrativeAccount();

        // Act
        repository.Delete(entity);

        // Assert
        contextMock.Verify(c => c.Remove(entity), Times.Once);
        contextMock.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public void GetAll_Should_Return_All_Entities()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entities = new List<AdministrativeAccount> { new AdministrativeAccount(), new AdministrativeAccount() };
        var dbSetMock = Usings.MockDbSet(entities);
        contextMock.Setup(c => c.Set<AdministrativeAccount>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.FindAll();

        // Assert
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public void GetById_Should_Return_Entity_With_Correct_Id()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entityId = Guid.NewGuid();
        var entity = new AdministrativeAccount { Id = entityId };

        contextMock.Setup(c => c.Set<AdministrativeAccount>().Find(entityId)).Returns(entity);

        // Act
        var result = repository.GetById(entityId);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public void Find_Should_Return_Entities_Matching_Expression()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entities = new List<AdministrativeAccount> { new AdministrativeAccount { Name = "Entity1" }, new AdministrativeAccount { Name = "Entity2" } };
        var dbSetMock = Usings.MockDbSet(entities);
        contextMock.Setup(c => c.Set<AdministrativeAccount>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Find(e => e.Name == "Entity1");

        // Assert
        Assert.Single(result);
        Assert.Equal("Entity1", result.First().Name);
    }

    [Fact]
    public void Exists_Should_Return_True_If_Entities_Match_Expression()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entities = new List<AdministrativeAccount> { new AdministrativeAccount { Name = "Entity1" }, new AdministrativeAccount { Name = "Entity2" } };
        var dbSetMock = Usings.MockDbSet(entities);
        contextMock.Setup(c => c.Set<AdministrativeAccount>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(e => e.Name == "Entity1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Exists_Should_Return_False_If_No_Entities_Match_Expression()
    {
        // Arrange
        var repository = new TestRepository(contextMock.Object);
        var entities = new List<AdministrativeAccount> { new AdministrativeAccount { Name = "Entity1" }, new AdministrativeAccount { Name = "Entity2" } };
        var dbSetMock = Usings.MockDbSet(entities);
        contextMock.Setup(c => c.Set<AdministrativeAccount>()).Returns(dbSetMock.Object);

        // Act
        var result = repository.Exists(e => e.Name == "Entity3");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void FindAllSorted_Should_Return_All_Entities_Sorted_By_Ascending_Specified_DefaultProperty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase_Default_Property_Sorted_By_Ascending").Options;
        using (var context = new RegisterContextAdministrative(options))
        {
            // Inserindo dados de exemplo no contexto
            var entities = MockAdministrativeAccount.Instance.GetListFaker();
            context.Set<AdministrativeAccount>().AddRange(entities);
            context.SaveChanges();

            // Criando o repositório com o contexto real
            var repository = new TestRepository(context);

            // Act
            var result = repository.FindAllSorted();

            // Assert
            var sortedEntities = entities.ToList();
            Assert.Equal(sortedEntities.Count, result.Count());
            Assert.Equal(sortedEntities.First().Id, result.First().Id);
            Assert.Equal(sortedEntities.Last().Id, result.Last().Id);
        }
    }
      

    [Fact]
    public void FindAllSorted_Should_Return_All_Entities_Sorted_By_Ascending_Specified_Property()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase_Property__Sorted_By_Ascending").Options;
        using (var context = new RegisterContextAdministrative(options))
        {
            // Inserindo dados de exemplo no contexto
            var entities = MockAdministrativeAccount.Instance.GetListFaker();
            context.Set<AdministrativeAccount>().AddRange(entities);
            context.SaveChanges();

            // Criando o repositório com o contexto real
            var repository = new TestRepository(context);

            // Act
            var result = repository.FindAllSorted(nameof(AdministrativeAccount.Name), SortOrder.Ascending);

            // Assert
            var sortedEntities = entities.OrderBy(e => e.Name).ToList();
            Assert.Equal(sortedEntities.Count, result.Count());
            Assert.Equal(sortedEntities.First().Id, result.First().Id);
            Assert.Equal(sortedEntities.Last().Id, result.Last().Id);
        }
    }

    [Fact]
    public void FindAllSorted_Should_Return_All_Entities_Sorted_By_Desscending_Specified_Property()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase_Property__Sorted_By_Descending").Options;
        using (var context = new RegisterContextAdministrative(options))
        {
            // Inserindo dados de exemplo no contexto
            var entities = MockAdministrativeAccount.Instance.GetListFaker();
            context.Set<AdministrativeAccount>().AddRange(entities);
            context.SaveChanges();

            // Criando o repositório com o contexto real
            var repository = new TestRepository(context);

            // Act
            var result = repository.FindAllSorted(nameof(AdministrativeAccount.Name), SortOrder.Descending);

            // Assert
            var sortedEntities = entities.OrderByDescending(e => e.Name).ToList();
            Assert.Equal(sortedEntities.Count, result.Count());
            Assert.Equal(sortedEntities.First().Id, result.First().Id);
            Assert.Equal(sortedEntities.Last().Id, result.Last().Id);
        }
    }


    [Fact]
    public void FindAllSorted_Should_Return_All_Entities_Sorted_By_Ascending_Specified_Navigation()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase_Sorted_By_Ascending").Options;
        using (var context = new RegisterContextAdministrative(options))
        {
            // Inserindo dados de exemplo no contexto
            var entities = MockAdministrativeAccount.Instance.GetListFaker();
            context.Set<AdministrativeAccount>().AddRange(entities);
            context.SaveChanges();

            // Criando o repositório com o contexto real
            var repository = new TestRepository(context);

            // Act
            var result = repository.FindAllSorted(nameof(AdministrativeAccount.Login.Email), SortOrder.Ascending);

            // Assert
            var sortedEntities = entities.OrderBy(e => e.Login.Email).ToList();
            Assert.Equal(sortedEntities.Count, result.Count());
            Assert.Equal(sortedEntities.First().Id, result.First().Id);
            Assert.Equal(sortedEntities.Last().Id, result.Last().Id);
        }
    }

    [Fact]
    public void FindAllSorted_Should_Return_All_Entities_Sorted_By_Descending_Specified_Navigation()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContextAdministrative>().UseInMemoryDatabase(databaseName: "TestDatabase_Sorted_By_Descending").Options;
        using (var context = new RegisterContextAdministrative(options))
        {
            // Inserindo dados de exemplo no contexto
            var entities = MockAdministrativeAccount.Instance.GetListFaker();
            context.Set<AdministrativeAccount>().AddRange(entities);
            context.SaveChanges();

            // Criando o repositório com o contexto real
            var repository = new TestRepository(context);

            // Act
            var result = repository.FindAllSorted(nameof(AdministrativeAccount.Login.Email), SortOrder.Descending);

            // Assert
            var sortedEntities = entities.OrderByDescending(e => e.Login.Email).ToList();
            Assert.Equal(sortedEntities.Count, result.Count());
            Assert.Equal(sortedEntities.First().Id, result.First().Id);
            Assert.Equal(sortedEntities.Last().Id, result.Last().Id);
        }
    }

}
