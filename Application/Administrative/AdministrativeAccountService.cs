using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using Application.Shared.Dto;
using AutoMapper;
using Domain.Administrative.Agreggates;
using Domain.Core;
using Repository.Interfaces;

namespace Application.Administrative;
public class AdministrativeAccountService : ServiceBase<AdministrativeAccountDto, AdministrativeAccount>, IService<AdministrativeAccountDto>, IAdministrativeAccountService, IAuthenticationService
{
    public AdministrativeAccountService(IMapper mapper, IRepository<AdministrativeAccount> customerRepository) : base(mapper, customerRepository)
    {

    }
    public override AdministrativeAccountDto Create(AdministrativeAccountDto dto)
    {
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
        var account = this.Mapper.Map<AdministrativeAccount>(dto);
        this.Repository.Update(account);
        return this.Mapper.Map<AdministrativeAccountDto>(account);
    }
    public override bool Delete(AdministrativeAccountDto dto)
    {
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

    public bool Authentication(LoginDto dto)
    {
        bool credentialsValid = false;

        var user = this.Repository.Find(u => u.Login.Email.Equals(dto.Email)).FirstOrDefault();
        if (user == null)
            throw new ArgumentException("Usuário inexistente!");
        else
        {
            credentialsValid = user != null && !String.IsNullOrEmpty(user.Login.Password) && !String.IsNullOrEmpty(user.Login.Email) && (Crypto.GetInstance.Decrypt(user.Login.Password).Equals(dto.Password));
        }

        if (credentialsValid)
            return true;

        throw new ArgumentException("Usuário Inválido!");
    }
}