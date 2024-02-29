using Application.Account.Dto;
using AutoMapper;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Repository;

namespace Application.Account;
public class MerchantService : ServiceBase<MerchantDto, Merchant>, IService<MerchantDto>
{
    private IRepository<Flat> FlatRepository { get; set; }
    public MerchantService(IMapper mapper, IRepository<Merchant> merchantRepository, IRepository<Flat> flatRepository) : base(mapper, merchantRepository)
    {
        FlatRepository = flatRepository;
    }
    public override MerchantDto Create(MerchantDto obj)
    {
        throw new NotImplementedException();
    }

    public override List<MerchantDto> FindAll(Guid idUsuario)
    {
        throw new NotImplementedException();
    }
    public override MerchantDto FindById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override MerchantDto Update(MerchantDto obj)
    {
        throw new NotImplementedException();
    }
    public override bool Delete(MerchantDto obj)
    {
        throw new NotImplementedException();
    }
}
