import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AlbumService } from './album.service';
import { Album } from '../../model';

describe('AlbumService', () => {
  let service: AlbumService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AlbumService]
    });

    service = TestBed.inject(AlbumService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch albums from API via GET', () => {
    // Arrange
    const mockAlbums: Album[] = [
      { id: '1', name: 'Album 1' },
      { id: '2', name: 'Album 2' }
    ];

    // Act
    service.getAllAlbum().subscribe((albums: Album[]) => {
      // Assert
      expect(albums).toEqual(mockAlbums);
    });

    // Assert
    const req = httpMock.expectOne(`album`);
    expect(req.request.method).toBe('GET');
    req.flush(mockAlbums);
    httpMock.verify();
  });
});
