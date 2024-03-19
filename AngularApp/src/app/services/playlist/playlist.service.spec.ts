import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Playlist } from '../../model';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from 'src/app/interceptors/http.interceptor.service';
import { PlaylistService } from '..';

describe('PlaylistService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PlaylistService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }]
    });
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([PlaylistService], (service: PlaylistService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('getAllPlaylist should send a get request to the api/playlist endpoint', inject(
    [PlaylistService, HttpTestingController],
    (service: PlaylistService, httpMock: HttpTestingController) => {
        const mockResponse: Playlist[] = [
          { id: '1', name: 'Playlist 1', backdrop: 'http://backdrop1.jpg', musics: [] },
          { id: '2', name: 'Playlist 2', backdrop: 'http://backdrop2.jpg', musics: [] }
        ];
      service.getAllPlaylist().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/playlist';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getPlaylistById should send a get request to the api/playlist endpoint', inject(
    [PlaylistService, HttpTestingController],
    (service: PlaylistService, httpMock: HttpTestingController) => {
        const mockResponse: Playlist = { id: '1', name: 'Playlist 1', backdrop: 'http://backdrop1.jpg', musics: [] };
      service.getPlaylistById('1').subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/playlist/1';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
