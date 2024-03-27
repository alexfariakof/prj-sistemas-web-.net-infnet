using Application.Account.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Account.Profile;
public class MusicProfile : AutoMapper.Profile
{
    public MusicProfile() 
    {
        CreateMap<MusicDto, Music>()
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.AlbumId ))
            .ReverseMap();

        CreateMap<Music, MusicDto>()
            .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.Album.Id))
            .ForMember(dest => dest.AlbumBackdrop, opt => opt.MapFrom(src => src.Album.Backdrop))
            .ForMember(dest => dest.BandId, opt => opt.MapFrom(src => src.Band.Id))
            .ForMember(dest => dest.BandBackDrop, opt => opt.MapFrom(src => src.Band.Backdrop))
            .ReverseMap();

        CreateMap<AlbumDto, Album>()
            .ReverseMap();
        CreateMap<BandDto, Band>()
            .ReverseMap();
    }
}
