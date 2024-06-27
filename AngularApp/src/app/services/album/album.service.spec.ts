import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { AlbumService } from './album.service';
import { Album } from '../../model';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../interceptors/http.interceptor.service';
import { MockAlbum } from '../../__mocks__';

describe('AlbumService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [AlbumService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([AlbumService], (service: AlbumService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('getAllAlbum should send a get request to the api/album endpoint', inject(
    [AlbumService, HttpTestingController],
    (service: AlbumService, httpMock: HttpTestingController) => {
        const mockResponse: Album[] = MockAlbum.instance().generateAlbumList(2);

      service.getAllAlbum().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/album';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getAlbumById should send a get request to the api/album endpoint', inject(
    [AlbumService, HttpTestingController],
    (service: AlbumService, httpMock: HttpTestingController) => {

      const mockResponse: Album = MockAlbum.instance().getFaker();
      const mockAlbumId = mockResponse.id;

      service.getAlbumById(mockAlbumId).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/album/${mockAlbumId}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));


});
