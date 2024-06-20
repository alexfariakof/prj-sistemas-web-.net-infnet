using Application.Streaming.Dto;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class PlaylistPersonalProfile : AutoMapper.Profile
{
    public PlaylistPersonalProfile() 
    {
        CreateMap<PlaylistPersonal, PlaylistPersonalDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ReverseMap();
        CreateMap<PlaylistPersonalDto, PlaylistPersonal>()
            .ForMember(dest => dest.DtCreated, opt => opt.MapFrom(src => DateTime.Now))
            .ReverseMap();
        CreateMap<MusicDto, Music>().ReverseMap();
    }
}
