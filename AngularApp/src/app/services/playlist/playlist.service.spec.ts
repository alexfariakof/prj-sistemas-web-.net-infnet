import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Playlist } from '../../model';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from 'src/app/interceptors/http.interceptor.service';
import { PlaylistService } from '..';
import { MockPlaylist } from 'src/app/__mocks__';

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
        const mockResponse: Playlist[] = MockPlaylist.instance().generatePlaylistList(3);
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
        const mockResponse: Playlist = MockPlaylist.instance().getFaker();
      service.getPlaylistById(mockResponse.id as string).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/playlist/${ mockResponse.id }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
