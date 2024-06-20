import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { Music } from '../../model';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../interceptors/http.interceptor.service';
import { MusicService } from '..';
import { MockMusic } from '../../__mocks__';


describe('MusicService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [MusicService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([MusicService], (service: MusicService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('getAllMusic should send a get request to the api/music endpoint', inject(
    [MusicService, HttpTestingController],
    (service: MusicService, httpMock: HttpTestingController) => {
        const mockResponse: Music[] = [
          {
            id: '1', name: 'Song 1', duration: 180,
            url: 'http://music1.mp3'
          },
          {
            id: '2', name: 'Song 2', duration: 200,
            url: 'http://music2.mp3'
          }
        ];

      service.getAllMusic().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/music';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getMusicById should send a get request to the api/music endpoint', inject(
    [MusicService, HttpTestingController],
    (service: MusicService, httpMock: HttpTestingController) => {
        const mockResponse: Music = MockMusic.instance().getFaker();
        const mockMusicId: string = mockResponse.id as string;

      service.getMusicById(mockMusicId).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/music/${ mockMusicId }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('searchMusic should send a get request to the api/music/search endpoint', inject(
    [MusicService, HttpTestingController],
    (service: MusicService, httpMock: HttpTestingController) => {
        const mockResponse: Music[] = MockMusic.instance().generateMusicList(3);

      service.searchMusic('teste').subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/music/search/teste`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));
});
