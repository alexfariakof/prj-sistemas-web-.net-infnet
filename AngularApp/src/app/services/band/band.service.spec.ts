import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BandService } from './band.service';
import { Band } from '../../model';

describe('BandService', () => {
  let service: BandService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [BandService]
    });

    service = TestBed.inject(BandService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch bands from API via GET', () => {
    // Arrange
    const mockBands: Band[] = [
      { id: '1', name: 'Band 1', description: 'Description 1', backdrop: 'Backdrop 1', album: {
        id: '1', name: 'Album 1',
        bandId: '0001'
      } },
      { id: '2', name: 'Band 2', description: 'Description 2', backdrop: 'Backdrop 2', album: {
        id: '2', name: 'Album 2',
        bandId: '0002'
      } }
    ];

    // Act
    service.getAllBand().subscribe((bands: Band[]) => {
      // Assert
      expect(bands).toEqual(mockBands);
    });

    // Assert
    const req = httpMock.expectOne(`band`);
    expect(req.request.method).toBe('GET');
    req.flush(mockBands);
  });
});
