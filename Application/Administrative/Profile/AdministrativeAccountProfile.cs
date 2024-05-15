using Application.Administrative.Dto;
using Domain.Administrative.Agreggates;
using Domain.Core.ValueObject;

namespace Application.Administrative.Profile;
public class AdministrativeAccountProfile : AutoMapper.Profile
{
    public AdministrativeAccountProfile() 
    {
        CreateMap<AdministrativeAccountDto, AdministrativeAccount>()
            .ForMember(dest => dest.PerfilType, opt =>  opt.MapFrom(src => (int)src.PerfilType))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => new Login() { Email = src.Email, Password = src.Password }))
            .ReverseMap();

        CreateMap<AdministrativeAccount, AdministrativeAccountDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Login.Email))
            .ForMember(dest => dest.PerfilType, opt => opt.MapFrom(src => (int)src.PerfilType.Id))
            .AfterMap((s, d) =>
            {
                d.Password = "********";
            });

        CreateMap<BasePerfil, PerfilDto>().ReverseMap();
        CreateMap<PerfilDto, BasePerfil>().ReverseMap();
    }
}
