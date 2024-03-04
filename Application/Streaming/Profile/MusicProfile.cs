using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Account.Profile;
public class MusicProfile : AutoMapper.Profile
{
    public MusicProfile() 
    {
        CreateMap<MusicDto, Music>()
            //.ForMember(dest => dest.Flats, opt => opt.MapFrom(src => new List<Flat> { new Flat { Id = src.FlatId } }))
            .ForMember(dest => dest.Playlists, opt => opt.MapFrom(src => new List<Playlist> { new Playlist { Id = src.PlaylistId } }))
            .ReverseMap();

        CreateMap<Music, MusicDto>()
            //.ForMember(dest => dest.FlatId, opt => opt.MapFrom(src => src.Flats.Any() ? src.Flats.First().Id : Guid.Empty))
            .ForMember(dest => dest.PlaylistId, opt => opt.MapFrom(src => src.Playlists.Any() ? src.Playlists.First().Id : Guid.Empty));
    }
}
