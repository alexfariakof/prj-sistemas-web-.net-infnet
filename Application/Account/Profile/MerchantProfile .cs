using Application.Account.Dto;
using Application.Transactions.Dto;
using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;

namespace Application.Account.Profile;
public class MerchantProfile : AutoMapper.Profile
{
    public MerchantProfile() 
    {
        CreateMap<MerchantDto, Merchant>();

        CreateMap<Merchant, MerchantDto>()
        .ForMember(x => x.Password, m => m.Ignore()) 
        .AfterMap((s, d) =>
         {
             var flat = s.Signatures?.FirstOrDefault(a => a.Active)?.Flat;

             if (flat != null)
                 d.FlatId = flat.Id;             
         });

        CreateMap<CardDto, Card>()
            .ForPath(x => x.Limit.Value, m => m.MapFrom(f => f.Limit))
            .ReverseMap();
    }
}
