using __mock__.Admin;
using Application.Administrative.Dto;
using Application.Administrative.Profile;
using AutoMapper;
using Domain.Administrative.Agreggates;

namespace Application.Administrative;
public class AdministrativeAccountProfileTest
{

    [Fact]
    public void Map_AdministrativeAccount_To_AdministrativeAccountDto_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<AdministrativeAccountProfile>(); }));

        var account = MockAdministrativeAccount.Instance.GetFaker();

        // Act
        var accountDto = mapper.Map<AdministrativeAccountDto>(account);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(accountDto.Id, account.Id);
        Assert.Equal(accountDto.Name, account.Name);
        Assert.Equal(accountDto.Email, account.Login.Email);
        Assert.Equal((int)accountDto.PerfilType, account.PerfilType.Id);
    }

    [Fact]
    public void Map_AdministrativeAccountDto_To_AdministrativeAccount_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<AdministrativeAccountProfile>(); }));

        var fakeAccount = MockAdministrativeAccount.Instance.GetFaker();
        var accountDto = MockAdministrativeAccount.Instance.GetNewDtoFromAdministrativeAccount(fakeAccount);

        // Act
        var account = mapper.Map<AdministrativeAccount>(accountDto);

        // Assert
        Assert.NotNull(account);
        Assert.Equal(accountDto.Id, account.Id);
        Assert.Equal(accountDto.Name, account.Name);
        Assert.Equal(accountDto.Email, account.Login.Email);
        Assert.Equal((int)accountDto.PerfilType, account.PerfilType.Id);
    }
}
