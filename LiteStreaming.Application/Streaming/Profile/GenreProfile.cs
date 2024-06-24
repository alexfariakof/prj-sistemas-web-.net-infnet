using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class GenreProfile : AutoMapper.Profile
{
    public GenreProfile() 
    {
        CreateMap<GenreDto, Genre>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
    }
}
