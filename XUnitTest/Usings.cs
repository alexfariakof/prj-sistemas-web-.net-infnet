global using Xunit;
global using __mock__;
using Microsoft.EntityFrameworkCore;
using Moq;
using Domain.Transactions.ValueObject;
using Domain.Core.Aggreggates;
using System.Linq.Expressions;
using Application.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Repository.Interfaces;
using Domain.Account.ValueObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Administrative.ValueObject;
public static class Usings
{
    public static Mock<DbSet<T>> MockDbSet<T>(List<T> data, DbContext? context = null)
    where T : class
    {
        var queryableData = data.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        dbSetMock
            .As<IQueryable<T>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => queryableData.GetEnumerator());
        dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
        dbSetMock
            .Setup(d => d.Update(It.IsAny<T>()))
            .Callback<T>(item =>
            { // Atualizar a entidade no contexto
                var existingItem = data.FirstOrDefault(i => i == item);
                if (existingItem != null)
                {
                    context.Attach(item);

                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChangesAsync().Wait();
                }
            });
        dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));
        return dbSetMock;
    }

    public static Mock<IRepository<T>> MockRepositorio<T>(List<T> _dataSet) where T : Base, new()
    {
        var _mock = new Mock<IRepository<T>>();

        _mock.Setup(repo => repo.Save(It.IsAny<T>()));
        _mock.Setup(repo => repo.Update(It.IsAny<T>()));
        _mock.Setup(repo => repo.Delete(It.IsAny<T>()));

        _mock.Setup(repo => repo.GetAll())
            .Returns(() => { return _dataSet.AsEnumerable(); });

        _mock.Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(
            (Guid id) =>
            {
                return _dataSet.SingleOrDefault(item => item.Id == id);
            });


        _mock.Setup(repo => repo.Find(It.IsAny<Expression<Func<T, bool>>>()))
            .Returns(
            (Expression<Func<T, bool>> expression) =>
            {
                return _dataSet.Where(expression.Compile());
            });

        _mock.Setup(repo => repo.Exists(It.IsAny<Expression<Func<T, bool>>>()));
        return _mock;
    }
    public static Mock<IRepository<CreditCardBrand>> MockDataSetCreditCardBrand()
    {
        var _mock = new Mock<IRepository<CreditCardBrand>>();

        _mock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns(
            (int id) =>
            {
                var creditCardBrandData = new List<CreditCardBrand>
                {
                    new CreditCardBrand((int)CardBrand.Invalid, CardBrand.Invalid.ToString()),
                    new CreditCardBrand((int)CardBrand.Visa, CardBrand.Visa.ToString()),
                    new CreditCardBrand((int)CardBrand.Mastercard, CardBrand.Mastercard.ToString()),
                    new CreditCardBrand((int)CardBrand.Amex, CardBrand.Amex.ToString()),
                    new CreditCardBrand((int)CardBrand.Discover, CardBrand.Discover.ToString()),
                    new CreditCardBrand((int)CardBrand.DinersClub, CardBrand.DinersClub.ToString()),
                    new CreditCardBrand((int)CardBrand.JCB, CardBrand.JCB.ToString())
                };

                return creditCardBrandData.SingleOrDefault(item => item.Id == id);
            });
        

        return _mock;
    }
    public static Mock<IRepository<PerfilUser>> MockDataSetUserType()
    {
        var _mock = new Mock<IRepository<PerfilUser>>();

        _mock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .Returns(
            (int id) =>
            {
                var userTypeData = new List<PerfilUser>
                {
                    new PerfilUser(PerfilUser.UserType.Admin),
                    new PerfilUser(PerfilUser.UserType.Customer),
                    new PerfilUser(PerfilUser.UserType.Merchant)
                };

                return userTypeData.SingleOrDefault(item => item.Id == id);
            });
        return _mock;
    }

    public static void SetupBearerToken(Guid userId, ControllerBase controller, PerfilUser.UserType userType = PerfilUser.UserType.Customer)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("UserType", new PerfilUser(userType).Description),
        };
        var identity = new ClaimsIdentity(claims, "UserId");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        httpContext.Request.Headers.Authorization = "Bearer " + Usings.GenerateJwtToken(userId, userType.ToString());

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    public static string GenerateJwtToken(Guid userId, string userType)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var signingConfigurations = new SigningConfigurations();
        configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString())
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] { 
            new Claim("UserId", userId.ToString()),
            new Claim("UserType", userType)
        };

        var token = new JwtSecurityToken(
            issuer: tokenConfigurations.Issuer,
            audience: tokenConfigurations.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(tokenConfigurations.Seconds),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}