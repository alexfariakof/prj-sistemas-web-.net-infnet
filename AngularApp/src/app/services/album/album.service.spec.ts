import { TestBed } from '@angular/core/testing';

import { AlbumService } from './album.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AlbumService', () => {
  let service: AlbumService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[AlbumService]
    });
    service = TestBed.inject(AlbumService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
