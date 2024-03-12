import { TestBed } from '@angular/core/testing';
import { BandService } from './band.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BandService', () => {
  let service: BandService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[BandService]
    });
    service = TestBed.inject(BandService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
