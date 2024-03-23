import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AlbumComponent } from './album.component';
import { ActivatedRoute } from '@angular/router';
import { AlbumService, PlaylistManagerService } from 'src/app/services';
import { of, throwError } from 'rxjs';
import { Album, Music, Playlist } from 'src/app/model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockAlbum } from 'src/app/__mocks__';

describe('AlbumComponent', () => {
  let component: AlbumComponent;
  let fixture: ComponentFixture<AlbumComponent>;
  let albumService: AlbumService;
  let playlistManagerService: PlaylistManagerService;
  let activatedRoute: ActivatedRoute;
  let mockAlbumId: string = '';

  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [AlbumComponent],
      imports: [RouterTestingModule, HttpClientTestingModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ mockAlbumId })
          }
        },
        AlbumService,
        PlaylistManagerService
      ]
    });
    fixture = TestBed.createComponent(AlbumComponent);
    component = fixture.componentInstance;
    albumService = TestBed.inject(AlbumService);
    playlistManagerService = TestBed.inject(PlaylistManagerService);
    activatedRoute = TestBed.inject(ActivatedRoute);

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve album on initialization', fakeAsync(() => {
    // Arrange
    const mockAlbum: Album = MockAlbum.instance().getFaker();
    const mockAlbumId = mockAlbum.id;
    spyOn(activatedRoute.params, 'pipe').and.returnValue(of({ albumId: mockAlbumId }));
    spyOn(albumService, 'getAlbumById').and.returnValue(of(mockAlbum));

    // Act
    component.getAlbum(mockAlbumId);
    fixture.detectChanges();
    tick();

    // Assert
    expect(albumService.getAlbumById).toHaveBeenCalledWith(mockAlbumId);
    expect(component.album).toEqual(mockAlbum);
    expect(component.musics).toEqual(mockAlbum.musics ?? []);
  }));

  it('should handle error when retrieving album', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Error retrieving album';
    spyOn(albumService, 'getAlbumById').and.returnValue(throwError(() => errorMessage));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(albumService.getAlbumById).toHaveBeenCalled();
    expect(component.album).toEqual({ id: '', name: '', bandId: '', backdrop: '' });
    expect(component.musics).toEqual([]);
  }));

  it('should handle null response when retrieving album', fakeAsync(() => {
    // Arrange
    const mockAlbum: Album | any = null;
    spyOn(albumService, 'getAlbumById').and.returnValue(of(mockAlbum));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(albumService.getAlbumById).toHaveBeenCalled();
    expect(component.album).toEqual({ id: '', name: '', bandId: '', backdrop: '' });
    expect(component.musics).toEqual([]);
  }));


  it('should add album to favorites', () => {
    // Arrange
    const mockAlbum: Album = MockAlbum.instance().getFaker();

    const mockPLaylist: Playlist = {
      name: mockAlbum.name,
      musics: mockAlbum.musics as Music[]
    }

    spyOn(playlistManagerService, 'createPlaylist').and.returnValue(of(mockPLaylist));

    // Act
    component.addToFavorites(mockAlbum);

    // Assert
    expect(playlistManagerService.createPlaylist).toHaveBeenCalledWith({
      name: mockAlbum.name,
      musics: mockAlbum.musics ?? []
    });
  });

  it('should add music from album to playlist', () => {
    // Arrange
    const mockPlaylistId = '1';
    const mockMusic: Music = { id: '1', name: 'Music 1', duration: 20, url: 'http://music1.mp3' };
    const mockPLaylist: Playlist = {
      id: mockPlaylistId,
      musics: [ mockMusic ]
    }

    spyOn(playlistManagerService, 'updatePlaylist').and.returnValue(of(mockPLaylist));

    // Act
    component.addMusicFromAlbumToPlaylist(mockPlaylistId, mockMusic);

    // Assert
    expect(playlistManagerService.updatePlaylist).toHaveBeenCalledWith({
      id: mockPlaylistId,
      musics: [mockMusic]
    });
  });
});
