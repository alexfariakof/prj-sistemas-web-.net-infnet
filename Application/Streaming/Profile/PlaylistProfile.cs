using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class PlaylistProfile : AutoMapper.Profile
{
    public PlaylistProfile() 
    {
        CreateMap<PlaylistDto, Playlist>().ReverseMap();
        CreateMap<Playlist, PlaylistDto>();
        CreateMap<MusicDto, Music>().ReverseMap();
    }
}
