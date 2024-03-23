using Application.Account.Dto;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;

namespace Application.Account.Profile;
public class PlaylistPersonalProfile : AutoMapper.Profile
{
    public PlaylistPersonalProfile() 
    {
        CreateMap<PlaylistPersonal, PlaylistPersonalDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ReverseMap();
        CreateMap<PlaylistPersonalDto, PlaylistPersonal>().ReverseMap();
        CreateMap<MusicDto, Music>().ReverseMap();

    }
}
