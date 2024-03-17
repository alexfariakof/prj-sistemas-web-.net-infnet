import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FavoritesBarComponent } from './favorites-bar.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MyPlaylistService } from 'src/app/services';
import { of, throwError } from 'rxjs';
import { Playlist } from 'src/app/model';

describe('FavoritesBarComponent', () => {
  let component: FavoritesBarComponent;
  let fixture: ComponentFixture<FavoritesBarComponent>;
  let myPlaylistService: MyPlaylistService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FavoritesBarComponent],
      imports: [HttpClientTestingModule],
      providers: [MyPlaylistService]
    });
    fixture = TestBed.createComponent(FavoritesBarComponent);
    component = fixture.componentInstance;
    myPlaylistService = TestBed.inject(MyPlaylistService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve list of playlists on initialization', fakeAsync(() => {
    // Arrange
    const mockPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [] },
      { id: '2', name: 'Playlist 2', musics: [] }
    ];

    spyOn(myPlaylistService, 'getAllPlaylist').and.returnValue(of(mockPlaylists));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getAllPlaylist).toHaveBeenCalled();
    expect(component.myPlaylist).toEqual(mockPlaylists);
  }));

  it('should handle error when retrieving list of playlists', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Error retrieving playlists';
    spyOn(myPlaylistService, 'getAllPlaylist').and.returnValue(throwError(() => errorMessage));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getAllPlaylist).toHaveBeenCalled();
    expect(component.myPlaylist).toEqual([]);
  }));

  it('should handle empty response when retrieving list of playlists', fakeAsync(() => {
    // Arrange
    const mockPlaylists: Playlist[] = [];
    spyOn(myPlaylistService, 'getAllPlaylist').and.returnValue(of(mockPlaylists));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(myPlaylistService.getAllPlaylist).toHaveBeenCalled();
    expect(component.myPlaylist).toEqual([]);
  }));

});
