using AutoFixture;
using LiteStreaming.AdministrativeApp.Models;

namespace LiteStreaming.XunitTest.__mock__.Admin;
public  class MockAlbumViewModel
{
    private static readonly Lazy<MockAlbumViewModel> _instance = new Lazy<MockAlbumViewModel>(() => new MockAlbumViewModel());
    private readonly Fixture fixture;

    private MockAlbumViewModel()
    {
        fixture = new Fixture();
    }

    public static MockAlbumViewModel Instance => _instance.Value;

    public AlbumViewModel GetFaker()
    {
        var musicViewModel = fixture.Create<AlbumViewModel>();
        return musicViewModel;
    }

    public List<AlbumViewModel> GetListFaker(int count = 3)
    {
        var musicViewModelList = new List<AlbumViewModel>();
        for (var i = 0; i < count; i++)
        {
            musicViewModelList.Add(MockAlbumViewModel.Instance.GetFaker());
        }
        return musicViewModelList;
    }
}
