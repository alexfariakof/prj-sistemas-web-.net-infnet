import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Album, Band, Music, Playlist } from 'src/app/model';
import { BandService, PlaylistManagerService } from 'src/app/services';
@Component({
  selector: 'app-band',
  templateUrl: './band.component.html',
  styleUrls: ['./band.component.css']
})
export class BandComponent implements OnInit {
  band: Band | any = {};
  albums: Album[] = [];
  hasStyle: string = '';
  myPlaylist: Playlist[] = [];


  constructor(
    private route: ActivatedRoute,
    private playlistManagerService: PlaylistManagerService,
    public bandService: BandService,
    ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bandId = params['bandId'];
      this.getBand(bandId);
    });
  }

  getBand = (bandId: string): void => {
    this.bandService.getBandById(bandId).subscribe({
      next: (response: Band) => {
        if (response != null) {
          this.band = response;
          this.albums = response.albums ?? [];
        }
        else {
          throw (response);
        }
      },
      error: (response: any) => {
        console.log(response.error);
      },
      complete() { }
    });
  }

  addToFavorites = (band: Band): void => {
    const playlist: Playlist = {
      name: 'Banda ' + band.name,
      musics: band.albums.flatMap(album => album.musics) as Music[]
    };

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

  addAlbumToFavorites = (album: Album): void => {
    const playlist: Playlist = {
      name: 'Album ' + album.name,
      musics: album.musics as Music[]
    };

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


}
