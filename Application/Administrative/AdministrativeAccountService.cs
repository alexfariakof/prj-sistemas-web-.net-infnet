using Application.Administrative.Dto;
using Application.Administrative.Interfaces;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Administrative.Agreggates;
using Repository.Interfaces;

namespace Application.Administrative;
public class AdministrativeAccountService : ServiceBase<AdministrativeAccountDto, AdministrativeAccount>, IService<AdministrativeAccountDto>, IAdministrativeAccountService
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
}