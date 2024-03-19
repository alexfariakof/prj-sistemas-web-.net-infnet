import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { ActivatedRoute } from '@angular/router';
import { PlaylistService } from '../../services';
import { of, throwError } from 'rxjs';
import { Playlist  } from '../../model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PlaylistComponent } from './playlist.component';

describe('PlaylistComponent', () => {
  let component: PlaylistComponent;
  let fixture: ComponentFixture<PlaylistComponent>;
  let playlistService: PlaylistService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaylistComponent],
      imports: [HttpClientTestingModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ playlistId: '1' })
          }
        },
        PlaylistService
      ]
    });
    fixture = TestBed.createComponent(PlaylistComponent);
    component = fixture.componentInstance;
    playlistService = TestBed.inject(PlaylistService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve playlist on initialization', fakeAsync(() => {
    // Arrange
    const mockPlaylist: Playlist = {
      id: '1',
      name: 'Playlist 1',
      musics: [
        {
          id: '1', name: 'Music 1', duration: 20,
          url: 'http://teste_music1.mp3'
        },
        {
          id: '2', name: 'Music 2', duration: 30,
          url: 'http://teste_music2.mp3'
        }
      ],
      backdrop: 'http://backdrop.jpg'
    };
    spyOn(playlistService, 'getPlaylistById').and.returnValue(of(mockPlaylist));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(playlistService.getPlaylistById).toHaveBeenCalledWith('1');
    expect(component.musics).toEqual(mockPlaylist.musics);
  }));

  it('should handle error when retrieving playlist', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Error retrieving playlist';
    spyOn(playlistService, 'getPlaylistById').and.returnValue(throwError(() => errorMessage));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(playlistService.getPlaylistById).toHaveBeenCalledWith('1');
    expect(component.musics).toEqual([]);
  }));

  it('should handle null response when retrieving playlist', fakeAsync(() => {
    // Arrange
    const mockPlaylist: Playlist| any = null;
    spyOn(playlistService, 'getPlaylistById').and.returnValue(of(mockPlaylist));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(playlistService.getPlaylistById).toHaveBeenCalledWith('1');
    expect(component.musics).toEqual([]);
  }));

});
