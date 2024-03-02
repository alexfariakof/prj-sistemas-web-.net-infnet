using Application.Account.Dto;
using Application.Authentication;
using Application.Transactions.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Microsoft.Extensions.Configuration;
using Moq;
using Repository;
using System.Linq.Expressions;

namespace Application.Account;
public class CustomerServiceTest
{
    private Mock<IMapper> mapperMock;
    private Mock<IRepository<Customer>> customerRepositoryMock;
    private Mock<IRepository<Flat>> flatRepositoryMock;
    private readonly CustomerService customerService;
    private readonly List<Customer> mockCustomerList = MockCustomer.GetListFaker(3);
    private readonly Mock<ICrypto> cryptoMock;
    public CustomerServiceTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var signingConfigurations = new SigningConfigurations();
        configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        mapperMock = new Mock<IMapper>();
        cryptoMock = new Mock<ICrypto>();
        customerRepositoryMock = Usings.MockRepositorio(mockCustomerList);
        flatRepositoryMock = Usings.MockRepositorio(new List<Flat>());

        customerService = new CustomerService(
            mapperMock.Object,
            customerRepositoryMock.Object,
            flatRepositoryMock.Object,
            signingConfigurations,
            tokenConfigurations
        );
    }

    [Fact]
    public void Create_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.GetFaker();
        var mockCard = MockCard.GetFaker();
        var mockFlat = MockFlat.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.Login.Email,
            Password = mockCustomer.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Addresses.Last().Zipcode,
                Street = mockCustomer.Addresses.Last().Street,
                Number = mockCustomer.Addresses.Last().Number,
                Neighborhood = mockCustomer.Addresses.Last().Neighborhood,
                City = mockCustomer.Addresses.Last().City,
                State = mockCustomer.Addresses.Last().State,
                Complement = mockCustomer.Addresses.Last().Complement,
                Country = mockCustomer.Addresses.Last().Country
            },
            FlatId = mockFlat.Id,
            Card = new CardDto
            {
                Limit = 1000,
                Number = mockCard.Number,
                Validate = mockCard.Validate.Value,
                CVV = mockCard.CVV
            }
        };

        flatRepositoryMock.Setup(repo => repo.GetById(mockFlat.Id)).Returns(mockFlat);
        mapperMock.Setup(mapper => mapper.Map<Card>(customerDto.Card)).Returns(mockCard);
        mapperMock.Setup(mapper => mapper.Map<Address>(customerDto.Address)).Returns(mockCustomer.Addresses.Last());
        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>())).Returns(customerDto);
        

        // Act
        var result = customerService.Create(customerDto);

        // Assert
        customerRepositoryMock.Verify(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        flatRepositoryMock.Verify(repo => repo.GetById(mockFlat.Id), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<Card>(customerDto.Card), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<Address>(customerDto.Address), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>()), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(customerDto.Name, result.Name);
        Assert.Equal(customerDto.Email, result.Email);
    }

    [Fact]
    public void Create_Customer_With_Existing_Email_Fails()
    {
        // Arrange
        var customerDto = new CustomerDto {  Email = "existing.email@example.com" };

        customerRepositoryMock.Setup(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(true);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => customerService.Create(customerDto));
        Assert.Equal("Usuário já existente na base.", exception.Message);
        customerRepositoryMock.Verify(repo => repo.Exists(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        flatRepositoryMock.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public void Create_Customer_With_Nonexistent_Flat_Fails()
    {
        // Arrange
        var customerDto = new CustomerDto();

        flatRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((Flat)null);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => customerService.Create(customerDto));
        Assert.Equal("Plano não existente ou não encontrado.", exception.Message);

        flatRepositoryMock.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Save(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public void FindAll_Customers_Successfully()
    {
        // Arrange
        var customerDtos = MockCustomer.GetDtoListFromCustomerList(mockCustomerList);
        var userId = mockCustomerList.First().Id;
        mapperMock.Setup(mapper => mapper.Map<List<CustomerDto>>(It.IsAny<List<Customer>>())).Returns(customerDtos.FindAll(c => c.Id.Equals(userId)));

        // Act
        var result = customerService.FindAll(userId);

        // Assert
        customerRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<List<CustomerDto>>(It.IsAny<List<Customer>>()), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(mockCustomerList.FindAll(c => c.Id.Equals(userId)).Count, result.Count);
        Assert.All(result, customerDto => Assert.Equal(userId, customerDto.Id));
    }

    [Fact]
    public void FindById_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = mockCustomerList.Last();
        var customerId = mockCustomer.Id;
        mockCustomer.Id = customerId;
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.Login.Email,
            Password = mockCustomer.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Addresses.Last().Zipcode,
                Street = mockCustomer.Addresses.Last().Street,
                Number = mockCustomer.Addresses.Last().Number,
                Neighborhood = mockCustomer.Addresses.Last().Neighborhood,
                City = mockCustomer.Addresses.Last().City,
                State = mockCustomer.Addresses.Last().State,
                Complement = mockCustomer.Addresses.Last().Complement,
                Country = mockCustomer.Addresses.Last().Country
            }
        };

        customerRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(mockCustomer);
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(It.IsAny<Customer>())).Returns(customerDto);

        // Act
        var result = customerService.FindById(customerId);

        // Assert
        customerRepositoryMock.Verify(repo => repo.GetById(customerId), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(mockCustomer), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(mockCustomer.Name, result.Name);
    }

    [Fact]
    public void Update_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.Login.Email,
            Password = mockCustomer.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Addresses.Last().Zipcode,
                Street = mockCustomer.Addresses.Last().Street,
                Number = mockCustomer.Addresses.Last().Number,
                Neighborhood = mockCustomer.Addresses.Last().Neighborhood,
                City = mockCustomer.Addresses.Last().City,
                State = mockCustomer.Addresses.Last().State,
                Complement = mockCustomer.Addresses.Last().Complement,
                Country = mockCustomer.Addresses.Last().Country
            }
        };

        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        customerRepositoryMock.Setup(repo => repo.Update(mockCustomer));
        mapperMock.Setup(mapper => mapper.Map<CustomerDto>(mockCustomer)).Returns(customerDto);

        // Act
        var result = customerService.Update(customerDto);

        // Assert
        mapperMock.Verify(mapper => mapper.Map<Customer>(customerDto), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Update(mockCustomer), Times.Once);
        mapperMock.Verify(mapper => mapper.Map<CustomerDto>(mockCustomer), Times.Once);

        Assert.NotNull(result);
        Assert.Equal(customerDto.Name, result.Name);
    }

    [Fact]
    public void Delete_Customer_Successfully()
    {
        // Arrange
        var mockCustomer = MockCustomer.GetFaker();
        var customerDto = new CustomerDto()
        {
            Name = mockCustomer.Name,
            Email = mockCustomer.Login.Email,
            Password = mockCustomer.Login.Password,
            CPF = mockCustomer.CPF,
            Birth = mockCustomer.Birth,
            Phone = mockCustomer.Phone.Number,
            Address = new AddressDto
            {
                Zipcode = mockCustomer.Addresses.Last().Zipcode,
                Street = mockCustomer.Addresses.Last().Street,
                Number = mockCustomer.Addresses.Last().Number,
                Neighborhood = mockCustomer.Addresses.Last().Neighborhood,
                City = mockCustomer.Addresses.Last().City,
                State = mockCustomer.Addresses.Last().State,
                Complement = mockCustomer.Addresses.Last().Complement,
                Country = mockCustomer.Addresses.Last().Country
            }
        };

        mapperMock.Setup(mapper => mapper.Map<Customer>(customerDto)).Returns(mockCustomer);
        customerRepositoryMock.Setup(repo => repo.Delete(mockCustomer));

        // Act
        var result = customerService.Delete(customerDto);

        // Assert
        mapperMock.Verify(mapper => mapper.Map<Customer>(customerDto), Times.Once);
        customerRepositoryMock.Verify(repo => repo.Delete(mockCustomer), Times.Once);

        Assert.True(result);
    }

    [Fact]
    public void Authentication_With_Valid_Credentials_Should_Return_AuthenticationDto()
    {
        // Arrange
        var mockCustomer = mockCustomerList.First();
        mockCustomer.Login.Password = "validPassword";
        var loginDto = new LoginDto { Email = mockCustomer.Login.Email, Password = "validPassword" };
        
        customerRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(mockCustomerList.Where(c => c.Login.Email.Equals(mockCustomer.Login.Email)));
        cryptoMock.Setup(crypto => crypto.Decrypt(It.IsAny<string>())).Returns(mockCustomer.Login.Password);

        // Act
        var result = customerService.Authentication(loginDto);

        // Assert
        customerRepositoryMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(result.AccessToken);
    }

    [Fact]
    public void Authentication_With_Invalid_Credentials_Should_Throw_Exception()
    {
        // Arrange
        var mockCustomer = mockCustomerList.First();
        var loginDto = new LoginDto { Email = "invalid.email@example.com", Password = "invalidPassword" };
        customerRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(mockCustomerList.Where(c => c.Login.Email.Equals(mockCustomer.Login.Email)));
        cryptoMock.Setup(crypto => crypto.Decrypt(It.IsAny<string>())).Returns(mockCustomer.Login.Password);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => customerService.Authentication(loginDto));
        Assert.Equal("Usuário Inválido!", exception.Message);
        customerRepositoryMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
    }
}