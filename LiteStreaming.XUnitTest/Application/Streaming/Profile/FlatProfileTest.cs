using Application.Streaming.Dto;
using AutoMapper;
using Domain.Streaming.Agreggates;

namespace Application.Streaming.Profile;
public sealed class FlatProfileTest
{

    [Fact]
    public void Map_Flat_To_FlatDto_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<FlatProfile>(); }));

        var flat = MockFlat.Instance.GetFaker();

        // Act
        var flatDto = mapper.Map<FlatDto>(flat);
        flatDto.FormattedValue = flat.Value.Value.ToString();

        // Assert
        Assert.NotNull(flat);
        Assert.Equal(flatDto.Id, flat.Id);
        Assert.Equal(flatDto.Name, flat.Name);
        Assert.Equal(flatDto.Description, flat.Description);
        //Assert.Equal(flatDto.Value, flat.Value.Value);
        Assert.Equivalent(flatDto.FormattedValue, new FlatDto() { Value = flat.Value }.FormattedValue);
    }

    [Fact]
    public void Map_FlatDto_To_Flat_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<FlatProfile>(); }));

        var fakeUser = MockUser.Instance.GetFaker();
        var flatDto = MockFlat.Instance.GetFakerDto(fakeUser.Id);

        // Act
        var flat = mapper.Map<Flat>(flatDto);

        // Assert
        Assert.NotNull(flat);
        Assert.Equal(flatDto.Id, flat.Id);
        Assert.Equal(flatDto.Name, flat.Name);
        Assert.Equal(flatDto.Description, flat.Description);
        Assert.Equal(flatDto.Value, flat.Value.Value);
    }
}
