import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Band } from '../../model';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from '../../interceptors/http.interceptor.service';
import { BandService } from './band.service';
import { MockBand } from '../../__mocks__';

describe('BandService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [BandService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }]
    });
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([BandService], (service: BandService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('getAllBand should send a get request to the api/band endpoint', inject(
    [BandService, HttpTestingController],
    (service: BandService, httpMock: HttpTestingController) => {
      const mockResponse: Band[] = MockBand.instance().generateBandList(3);


      service.getAllBand().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/band';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('getBandById should send a get request to the api/band endpoint', inject(
    [BandService, HttpTestingController],
    (service: BandService, httpMock: HttpTestingController) => {
      const mockResponse: Band =MockBand.instance().getFaker();
      const mockBandId = mockResponse.id;

      service.getBandById(mockBandId).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `api/band/${ mockBandId }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));
});
