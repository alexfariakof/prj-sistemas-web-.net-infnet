using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class AlbumProfile : AutoMapper.Profile
{
    public AlbumProfile() 
    {
        CreateMap<AlbumDto, Album>().ReverseMap();
        CreateMap<Album, AlbumDto>();
        CreateMap<MusicDto, Music>().ReverseMap();
    }
}
