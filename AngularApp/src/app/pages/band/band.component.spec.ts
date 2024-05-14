import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { BandComponent } from './band.component';
import { ActivatedRoute } from '@angular/router';
import { Album, Band, Music, Playlist } from '../../model';
import { MyPlaylistService, BandService, PlaylistManagerService } from '../../services';
import { of, throwError } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockBand, MockPlaylist, MockAlbum } from '../../__mocks__';
import { SharedModule } from 'src/app/components/shared.module';


describe('BandComponent', () => {
  let component: BandComponent;
  let fixture: ComponentFixture<BandComponent>;
  let bandService: BandService;
  let activatedRoute: ActivatedRoute;
  let playlistManagerService: PlaylistManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BandComponent],
      imports: [HttpClientTestingModule, RouterTestingModule, SharedModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            params: of({ bandId: '1' })
          }
        },
        MyPlaylistService,
        BandService
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BandComponent);
    component = fixture.componentInstance;
    bandService = TestBed.inject(BandService);
    activatedRoute = TestBed.inject(ActivatedRoute);
    playlistManagerService = TestBed.inject(PlaylistManagerService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve band information on initialization', fakeAsync(() => {
    // Arrange
    const mockBand: Band = MockBand.instance().getFaker();
    spyOn(bandService, 'getBandById').and.returnValue(of(mockBand));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(bandService.getBandById).toHaveBeenCalledWith('1');
    expect(component.band).toEqual(mockBand);
    expect(component.albums).toEqual(mockBand.albums ?? []);
  }));

  it('should handle error when retrieving band information', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Error retrieving band information';
    spyOn(bandService, 'getBandById').and.returnValue(throwError(() => errorMessage));

    // Act
    fixture.detectChanges();
    tick();

    // Assert
    expect(bandService.getBandById).toHaveBeenCalledWith('1');
    expect(component.band).toEqual({});
    expect(component.albums).toEqual([]);
  }));


  it('should add band to favorites', () => {
    // Arrange
    const mockBand: Band = MockBand.instance().getFaker();

    const mockPLaylist: Playlist = {
      name: mockBand.name,
      musics: mockBand.albums.flatMap(album => album.musics) as Music[]
    }

    spyOn(playlistManagerService, 'createPlaylist').and.returnValue(of(mockPLaylist));

    // Act
    component.addToFavorites(mockBand);

    // Assert
    expect(playlistManagerService.createPlaylist).toHaveBeenCalled();
  });

  it('addAlbumToFavorites should add music favorites', () => {
    // Arrange
    const mockPlaylist: Playlist = MockPlaylist.instance().getFaker();
    const mockAlbum: Album = MockAlbum.instance().getFaker();
    mockPlaylist.musics = mockAlbum.musics as Music[];
    const mockPlaylistId = mockPlaylist.id;

    spyOn(playlistManagerService, 'createPlaylist').and.returnValue(of(mockPlaylist));

    // Act
    component.addAlbumToFavorites(mockAlbum);

    // Assert
    expect(playlistManagerService.createPlaylist).toHaveBeenCalled();
  });

});
