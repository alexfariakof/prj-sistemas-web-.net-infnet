import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FavoritesBarComponent } from './favorites-bar.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PlaylistManagerService } from '../../services';
import { Playlist } from 'src/app/model';

describe('FavoritesBarComponent', () => {
  let component: FavoritesBarComponent;
  let fixture: ComponentFixture<FavoritesBarComponent>;
  let playlistManagerService: PlaylistManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FavoritesBarComponent],
      imports: [HttpClientTestingModule],
      providers: [PlaylistManagerService]
    });
    fixture = TestBed.createComponent(FavoritesBarComponent);
    component = fixture.componentInstance;
    playlistManagerService = TestBed.inject(PlaylistManagerService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize myPlaylist array from cached playlist', () => {
    // Arrange
    const cachedPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];
    spyOn(playlistManagerService.playlists$, 'subscribe').and.callFake(():any => {
      component.myPlaylist = cachedPlaylists;
      return cachedPlaylists;
    });

    // Act
    component.ngOnInit();

    // Assert
    expect(component.myPlaylist).toEqual(cachedPlaylists);
  });

  it('should removoPlaylist when is called', () => {
    // Arrange
    const playlistId = '1';
    spyOn(playlistManagerService, 'deletePlaylist').and.callThrough();

    // Act
    component.removoPlaylist(playlistId);

    // Assert
    expect(playlistManagerService.deletePlaylist).toHaveBeenCalledWith(playlistId);
  });

});
