using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using Application.Shared.Dto;
using AutoMapper;
using Domain.Administrative.Agreggates;
using Domain.Administrative.ValueObject;
using EasyCryptoSalt;
using Repository.Interfaces;

namespace Application.Administrative;
public class AdministrativeAccountService : ServiceBase<AdministrativeAccountDto, AdministrativeAccount>, IService<AdministrativeAccountDto>, IAdministrativeAccountService, IAdministrativeAuthenticationService
{
    private readonly ICrypto _crypto;
    public AdministrativeAccountService(IMapper mapper, IRepository<AdministrativeAccount> customerRepository, ICrypto crypto) : base(mapper, customerRepository)    
    {
        _crypto = crypto;
    }

    public override AdministrativeAccountDto Create(AdministrativeAccountDto dto)
    {
        IsValidPerfilUsuario(dto);

        if (this.Repository.Exists(x => x.Login != null && x.Login.Email == dto.Email))
            throw new ArgumentException("Usuário já cadastrado.");        

        var account = this.Mapper.Map<AdministrativeAccount>(dto);
        this.Repository.Save(account);
        var result = this.Mapper.Map<AdministrativeAccountDto>(account);
        return result;
    }

    public override AdministrativeAccountDto FindById(Guid id)
    {
        var account = this.Repository.GetById(id);
        var result = this.Mapper.Map<AdministrativeAccountDto>(account);
        return result;
    }

    public override List<AdministrativeAccountDto> FindAll(Guid userId)
    {
        var accounts = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
        var result = this.Mapper.Map<List<AdministrativeAccountDto>>(accounts);
        return result;
    }

    public override AdministrativeAccountDto Update(AdministrativeAccountDto dto)
    {
        IsValidPerfilUsuario(dto);
        var account = this.Mapper.Map<AdministrativeAccount>(dto);
        this.Repository.Update(account);
        return this.Mapper.Map<AdministrativeAccountDto>(account);
    }

    public override bool Delete(AdministrativeAccountDto dto)
    {
        IsValidPerfilUsuario(dto);
        var account = this.Mapper.Map<AdministrativeAccount>(dto);
        this.Repository.Delete(account);
        return true; 
    }

    public IEnumerable<AdministrativeAccountDto> FindAll()
    {
        var accounts = this.Repository.GetAll().AsEnumerable();
        var result = this.Mapper.Map<List<AdministrativeAccountDto>>(accounts);
        return result;

    }

    public AdministrativeAccountDto Authentication(LoginDto dto)
    {
        bool credentialsValid = false;
        var account = this.Repository.Find(u => u.Login.Email.Equals(dto.Email)).FirstOrDefault();
        if (account == null)
            throw new ArgumentException("Usuário inexistente!");
        else
        {
            credentialsValid = account is not null && !String.IsNullOrEmpty(account.Login.Password) && !String.IsNullOrEmpty(account.Login.Email) && (_crypto.IsEquals(dto.Password ?? "", account.Login.Password));
        }

        if (credentialsValid)
            return this.Mapper.Map<AdministrativeAccountDto>(account);

        throw new ArgumentException("Usuário Inválido!");
    }
    private void IsValidPerfilUsuario(AdministrativeAccountDto dto)
    {
        var administrador = this.Repository.Find(account => account.Id == dto.UsuarioId && account.PerfilType.Id == (int)Perfil.UserType.Admin).FirstOrDefault();
        if (administrador == null)
            throw new ArgumentException("Usuário não permitido a realizar operação.");

    }
}