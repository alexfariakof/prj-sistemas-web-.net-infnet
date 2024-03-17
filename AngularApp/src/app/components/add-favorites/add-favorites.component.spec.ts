import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AddFavoritesComponent } from './add-favorites.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MyPlaylistService } from 'src/app/services';
import { of, throwError } from 'rxjs';
import { Playlist } from 'src/app/model';

describe('AddFavoritesComponent', () => {
  let component: AddFavoritesComponent;
  let fixture: ComponentFixture<AddFavoritesComponent>;
  let myPlaylistService: MyPlaylistService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddFavoritesComponent],
      imports: [HttpClientTestingModule],
      providers: [MyPlaylistService]
    });
    fixture = TestBed.createComponent(AddFavoritesComponent);
    component = fixture.componentInstance;
    myPlaylistService = TestBed.inject(MyPlaylistService);
    fixture.detectChanges();
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
    component.ngOnInit();
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
    component.ngOnInit();
    tick();

    // Assert
    expect(myPlaylistService.getAllPlaylist).toHaveBeenCalled();
    expect(component.myPlaylist).toEqual([]);
  }));
});
