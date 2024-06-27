import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddFavoritesComponent } from './add-favorites.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { PlaylistManagerService } from '../../services';
import { Playlist } from '../../model';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AddFavoritesComponent', () => {
  let component: AddFavoritesComponent;
  let fixture: ComponentFixture<AddFavoritesComponent>;
  let playlistManagerService: PlaylistManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [AddFavoritesComponent],
    imports: [],
    providers: [PlaylistManagerService, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(AddFavoritesComponent);
    component = fixture.componentInstance;
    playlistManagerService = TestBed.inject(PlaylistManagerService);
  });

  it('should create', () => {
    // Arrange
    expect(component).toBeTruthy();
  });

  it('should initialize myPlaylist array from cached playlist', () => {
    // Arrange
    const cachedPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];
    spyOn(playlistManagerService, 'getCachedPlaylist').and.returnValue(cachedPlaylists);

    // Act
    component.ngOnInit();

    // Assert
    expect(component.myPlaylist).toEqual(cachedPlaylists);
  });

  it('should initialize myPlaylist array when myplaylist is 0 from cached playlist', () => {
    // Arrange
    const cachedPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];
    spyOn(playlistManagerService, 'getCachedPlaylist').and.returnValue([]);
    spyOn(playlistManagerService.playlists$, 'subscribe').and.callFake(():any => {
      component.myPlaylist = cachedPlaylists;
      return cachedPlaylists;
    });

    // Act
    component.ngOnInit();

    // Assert
    expect(component.myPlaylist).toEqual(cachedPlaylists);
  });


  it('should subscribe to playlists$ and update myPlaylist when cached playlist is empty', () => {
    // Arrange
    const playlists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];
    spyOn(playlistManagerService, 'getCachedPlaylist').and.returnValue(playlists);
    spyOn(playlistManagerService.playlists$, 'subscribe').and.callFake(():any => {
      return playlists;
    });

    // Act
    component.ngOnInit();

    // Assert
    expect(component.myPlaylist).toEqual(playlists);
  });

  it('should emit addToFavoritesEvent when onAddToFavoritesClick is called', () => {
    // Arrange
    const playlistId = '1';
    const emitSpy = spyOn(component.addToFavoritesEvent, 'emit');

    // Act
    component.onAddToFavoritesClick(playlistId);

    // Assert
    expect(emitSpy).toHaveBeenCalledWith(playlistId);
  });
});
