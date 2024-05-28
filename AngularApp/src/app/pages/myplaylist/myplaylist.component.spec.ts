import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { MyplaylistComponent } from './myplaylist.component';
import { ActivatedRoute } from '@angular/router';
import { MyPlaylistService, PlaylistManagerService } from '../..//services';
import { of, throwError } from 'rxjs';
import { Music, Playlist } from '../../model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MockPlaylist } from '../../__mocks__';
import { SharedModule } from 'src/app/components/shared.module';


describe('MyplaylistComponent', () => {
  let component: MyplaylistComponent;
  let fixture: ComponentFixture<MyplaylistComponent>;
  let myPlaylistService: MyPlaylistService;
  let activatedRoute: ActivatedRoute;
  let playlistManagerService: PlaylistManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyplaylistComponent],
      imports: [HttpClientTestingModule, SharedModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ playlistId: '1' })
          }
        },
        MyPlaylistService
      ]
    });
    fixture = TestBed.createComponent(MyplaylistComponent);
    component = fixture.componentInstance;
    myPlaylistService = TestBed.inject(MyPlaylistService);
    activatedRoute = TestBed.inject(ActivatedRoute);
    playlistManagerService = TestBed.inject(PlaylistManagerService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve playlist on initialization', fakeAsync(() => {
    // Arrange
    const mockPlaylist: Playlist = MockPlaylist.instance().getFaker();
    let mockPlaylistId: string  = mockPlaylist.id as string;
    spyOn(component.activeRoute.params, 'subscribe').and.callFake(():any => {
      component.playlistId = mockPlaylistId;
      return mockPlaylistId;
    });
    spyOn(myPlaylistService, 'getPlaylist').and.returnValue(of(mockPlaylist));

    // Act
    component.getMyplaylist(mockPlaylistId);
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getPlaylist).toHaveBeenCalledWith(mockPlaylistId);
    expect(component.musics).toEqual(mockPlaylist.musics);
  }));

  it('should handle error when retrieving playlist', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Error retrieving playlist';
    spyOn(myPlaylistService, 'getPlaylist').and.returnValue(throwError(() => errorMessage));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getPlaylist).toHaveBeenCalledWith('1');
    expect(component.musics).toEqual([]);
  }));

  it('should handle null response when retrieving playlist', fakeAsync(() => {
    // Arrange
    const mockPlaylist: Playlist| any = null;
    spyOn(myPlaylistService, 'getPlaylist').and.returnValue(of(mockPlaylist));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getPlaylist).toHaveBeenCalledWith('1');
    expect(component.musics).toEqual([]);
  }));

  it('addMusicToFavorites should add music to favorites', () => {
    // Arrange
    const mockPlaylist: Playlist = MockPlaylist.instance().getFaker();
    spyOn(component.route, 'navigate').and.callFake(():any  => {});
    spyOn(playlistManagerService, 'updatePlaylist').and.returnValue(of(mockPlaylist));

    // Act
    component.addMusicToFavorites(mockPlaylist.id, mockPlaylist.musics[0] as Music);

    // Assert
    expect(playlistManagerService.updatePlaylist).toHaveBeenCalled();
  });


});
