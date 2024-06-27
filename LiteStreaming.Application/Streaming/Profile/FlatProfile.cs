using Application.Streaming.Dto;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public class FlatProfile : AutoMapper.Profile
{
    public FlatProfile() 
    {
        CreateMap<FlatDto, Flat>().ReverseMap();
        CreateMap<Flat, FlatDto>().ReverseMap();
    }
}
