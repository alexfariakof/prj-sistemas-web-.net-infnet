import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { Playlist } from '../../model';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../interceptors/http.interceptor.service';
import { MyPlaylistService } from '..';
import { PlaylistCacheService } from './myplaylist.cache.service';
import { MockPlaylist } from '../../__mocks__';

describe('MyPlaylistService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [MyPlaylistService, PlaylistCacheService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([MyPlaylistService], (service: MyPlaylistService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('getAllPlaylist should send a get request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
        const mockResponse: Playlist[] = MockPlaylist.instance().generatePlaylistList(3);
      service.getAllPlaylist().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/customer/myplaylist';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getPlaylist should send a get request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: Playlist = MockPlaylist.instance().getFaker();

      service.getPlaylist(mockResponse.id as string).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/customer/myplaylist/${ mockResponse.id }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getPlaylist should send a get request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: Playlist = MockPlaylist.instance().getFaker();
;
      service.getPlaylist(mockResponse.id as string).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
    const expectedUrl = `api/customer/myplaylist/${ mockResponse.id }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('createPlaylist should send a post request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: Playlist = MockPlaylist.instance().getFaker();
;
      service.createPlaylist(mockResponse).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/customer/myplaylist';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('updatePlaylist should send a put request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: Playlist = MockPlaylist.instance().getFaker();
;
      service.updatePlaylist(mockResponse).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/customer/myplaylist';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('PUT');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('deletePlaylist should send a delete request to the api/customer/myplaylist endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: Playlist = MockPlaylist.instance().getFaker();
;
      service.deletePlaylist(mockResponse.id as string).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/customer/myplaylist/${ mockResponse.id }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('DELETE');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('removeMusicFromFavotites should send a delete request to the api/customer/myplaylist/music endpoint', inject(
    [MyPlaylistService, HttpTestingController],
    (service: MyPlaylistService, httpMock: HttpTestingController) => {
      const mockResponse: boolean = true;
      service.removeMusicFromFavotites('1', '1').subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/customer/myplaylist/1/music/1';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('DELETE');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
