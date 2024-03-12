import { TestBed } from '@angular/core/testing';

import { MyPlaylistService } from './myplaylist.service';
import { MusicService } from '../music/music.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MyPlaylistService', () => {
  let service: MyPlaylistService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[MusicService]
    });
    service = TestBed.inject(MyPlaylistService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
