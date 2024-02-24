using Application.Conta.Dto;
using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;

namespace Application.Conta.Profile;
public class CustomerProfile : AutoMapper.Profile
{
    public CustomerProfile() 
    {
        CreateMap<CustomerDto, Customer>();

        CreateMap<Customer, CustomerDto>()
        // Configura propiedade password para que seja nula. 
        .ForMember(x => x.Password, m => m.Ignore()) 
        .AfterMap((s, d) =>
         {
             var flat = s.Signatures?.FirstOrDefault(a => a.Active)?.Flat;

             if (flat != null)
                 d.FlatId = flat.Id;

             d.Password = "********";
             
         });

        CreateMap<CardDto, Card>()
            .ForPath(x => x.Limit.Value, m => m.MapFrom(f => f.Limit))
            .ReverseMap();
    }
}
