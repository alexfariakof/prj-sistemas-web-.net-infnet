import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MusicService } from './music.service';
import { Music } from 'src/app/model';

describe('MusicService', () => {
  let service: MusicService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MusicService]
    });

    service = TestBed.inject(MusicService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all music from API via GET', () => {
    // Arrange
    const mockMusicList: Music[] = [
      { id: '1', name: 'Song 1', duration: 180 },
      { id: '2', name: 'Song 2', duration: 200 }
    ];

    // Act
    service.getAllMusic().subscribe((musicList: Music[]) => {
      // Assert
      expect(musicList).toEqual(mockMusicList);
    });

    // Assert
    const req = httpMock.expectOne('music');
    expect(req.request.method).toBe('GET');
    req.flush(mockMusicList);
  });
});
