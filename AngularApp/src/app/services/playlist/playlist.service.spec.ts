import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PlaylistService } from './playlist.service';
import { Playlist } from '../../model';

describe('PlaylistService', () => {
  let service: PlaylistService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PlaylistService]
    });

    service = TestBed.inject(PlaylistService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all playlists from API via GET', () => {
    // Arrange
    const mockPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [] },
      { id: '2', name: 'Playlist 2', musics: [] }
    ];

    // Act
    service.getAllPlaylist().subscribe((playlists: Playlist[]) => {
      // Assert
      expect(playlists).toEqual(mockPlaylists);
    });

    // Assert
    const req = httpMock.expectOne('playlist');
    expect(req.request.method).toBe('GET');
    req.flush(mockPlaylists);
  });
});
