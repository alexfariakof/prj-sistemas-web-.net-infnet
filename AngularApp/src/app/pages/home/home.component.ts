import { BandService } from './../../services/band/band.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Album, Band, Music, Playlist } from '../../model';
import { AlbumService, PlaylistManagerService, PlaylistService } from '../../services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class HomeComponent implements OnInit {
  hasStyle: string = '';
  playlist: Playlist[] = [];
  bands: Band[] = [];
  albums: Album[] = [];

  constructor(
    public playlistService: PlaylistService,
    public bandService: BandService,
    public albumService: AlbumService,
    private playlistManagerService: PlaylistManagerService) { }

  ngOnInit() : void {
    this.getListOfPlaylist();
    this.getListOfBands();
    this.getListOfAlbums();
  }

  getListOfPlaylist = (): void => {
    this.playlistService.getAllPlaylist()
    .subscribe({
      next: (response: Playlist[]) => {
        if (response != null)
          this.playlist = response;
      },
      error: (response: any) => {
        console.log(response.error);
      }
    });
  }

  getListOfBands = (): void => {
    this.bandService.getAllBand()
    .subscribe({
      next: (response: Band[]) => {
        if (response)
          this.bands = response;
      },
      error: (response: any) => {
        console.log(response.error);
      }
    });
  }

  getListOfAlbums = (): void => {
    this.albumService.getAllAlbum()
    .subscribe({
      next: (response: Album[]) => {
        if (response)
          this.albums = response;
      },
      error: (response: any) => {
        console.log(response.error);
      }
    });
  }

  addToFavorites = (playlist: Playlist): void => {
    this.playlistManagerService.createPlaylist(playlist).subscribe({
      next: (response: Playlist) => {
        if (response)
          alert('Playlist Criada com sucesso!');
      },
      error: (response: any) => {
        alert(response.error);
      }
    });
  }

  addBandToFavorites = (band: Band): void => {
    const playlist: Playlist = {
      name: 'Banda ' + band.name,
      musics: band.albums.flatMap(album => album.musics) as Music[]
    };
    this.addToFavorites(playlist);
  }

  addAlbumToFavorites = (album: Album): void => {
    const playlist: Playlist = {
      name: album.name,
      musics: album.musics as Music[]
    };
    this.addToFavorites(playlist);
  }
}
