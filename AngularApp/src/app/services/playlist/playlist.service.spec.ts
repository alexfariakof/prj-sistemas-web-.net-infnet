import { TestBed } from '@angular/core/testing';

import { PlaylistService } from './playlist.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('PlaylistService', () => {
  let service: PlaylistService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[PlaylistService]
    });
    service = TestBed.inject(PlaylistService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
