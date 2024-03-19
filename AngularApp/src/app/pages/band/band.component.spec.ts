import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { BandComponent } from './band.component';
import { ActivatedRoute } from '@angular/router';
import { Band } from 'src/app/model';
import { MyPlaylistService, BandService } from 'src/app/services';
import { of, throwError } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('BandComponent', () => {
  let component: BandComponent;
  let fixture: ComponentFixture<BandComponent>;
  let bandService: BandService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BandComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
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
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve band information on initialization', fakeAsync(() => {
    // Arrange
    const mockBand: Band = {
      id: '1',
      name: 'Band 1',
      description: 'Description',
      backdrop: 'Backdrop',
      albums: [{
        id: '1',
        name: 'Album 1',
        bandId: '1',
        musics: [{
          id: '1', name: 'Song 1', duration: 300,
          url: ''
        }],
        backdrop: ''
      }]
    };
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

});
