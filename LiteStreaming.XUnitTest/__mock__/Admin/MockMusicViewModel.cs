using AutoFixture;
using LiteStreaming.AdministrativeApp.Models;

namespace LiteStreaming.XunitTest.__mock__.Admin;
public  class MockMusicViewModel
{
    private static readonly Lazy<MockMusicViewModel> _instance = new Lazy<MockMusicViewModel>(() => new MockMusicViewModel());
    private readonly Fixture fixture;

    private MockMusicViewModel()
    {
        fixture = new Fixture();
    }

    public static MockMusicViewModel Instance => _instance.Value;

    public MusicViewModel GetFaker()
    {
        var musicViewModel = fixture.Create<MusicViewModel>();
        return musicViewModel;
    }

    public List<MusicViewModel> GetListFaker(int count = 3)
    {
        var musicViewModelList = new List<MusicViewModel>();
        for (var i = 0; i < count; i++)
        {
            musicViewModelList.Add(MockMusicViewModel.Instance.GetFaker());
        }
        return musicViewModelList;
    }
}
