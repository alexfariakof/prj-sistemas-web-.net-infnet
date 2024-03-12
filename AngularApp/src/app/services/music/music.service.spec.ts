import { TestBed } from '@angular/core/testing';
import { MusicService } from './music.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MusicService', () => {
  let service: MusicService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[MusicService]
    });
    service = TestBed.inject(MusicService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
