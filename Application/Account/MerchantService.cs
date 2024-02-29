using Application.Account.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Repository;

namespace Application.Account;
public class MerchantService : ServiceBase<MerchantDto, Merchant>, IService<MerchantDto>
{
    private IRepository<Flat> FlatRepository { get; set; }
    public MerchantService(IMapper mapper, IRepository<Merchant> merchantRepository, IRepository<Flat> flatRepository) : base(mapper, merchantRepository)
    {
        FlatRepository = flatRepository;
    }
    public override MerchantDto Create(MerchantDto dto)
    {
        if (this.Repository.Exists(x => x.Login != null && x.Login.Email == dto.Email))
            throw new Exception("Usuário já existente na base.");


        Flat flat = this.FlatRepository.GetById(dto.FlatId);

        if (flat == null)
            throw new Exception("Plano não existente ou não encontrado.");

        Card card = this.Mapper.Map<Card>(dto.Card);

        Merchant merchant = new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CPF = dto.CPF,
            CNPJ= dto.CNPJ,
            Phone = dto.Phone,
            Login = new()
            {
                Email = dto.Email ?? "",
                Password = dto.Password ?? ""
            }
        };

        Address address = this.Mapper.Map<Address>(dto.Address);

        merchant.CreateAccount(merchant, address, flat, card);

        this.Repository.Save(merchant);
        var result = this.Mapper.Map<MerchantDto>(merchant);

        return result;
    }
    public override MerchantDto FindById(Guid id)
    {
        var merchant = this.Repository.GetById(id);
        var result = this.Mapper.Map<MerchantDto>(merchant);
        return result;
    }

    public override List<MerchantDto> FindAll(Guid userId)
    {
        var merchants = this.Repository.GetAll().Where(c => c.Id == userId).ToList();
        var result = this.Mapper.Map<List<MerchantDto>>(merchants);
        return result;
    }

    public override MerchantDto Update(MerchantDto dto)
    {
        var merchant = this.Mapper.Map<Merchant>(dto);
        this.Repository.Update(merchant);
        return this.Mapper.Map<MerchantDto>(merchant);
    }

    public override bool Delete(MerchantDto dto)
    {
        var merchant = this.Mapper.Map<Merchant>(dto);
        this.Repository.Delete(merchant);
        return true;
    }
}