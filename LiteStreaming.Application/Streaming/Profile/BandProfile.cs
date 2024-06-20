using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class BandProfile : AutoMapper.Profile
{
    public BandProfile() 
    {
        CreateMap<BandDto, Band>()
            .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums ))
            .ReverseMap();

        CreateMap<Band, BandDto>()
            .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums))
            .AfterMap((s, d) =>
            {

            });

        CreateMap<MusicDto, Music>().ReverseMap();
        CreateMap<AlbumDto, Album>().ReverseMap();
    }
}
