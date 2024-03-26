import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Album, Music, Playlist} from '../../model';
import { AlbumService, PlaylistManagerService } from '../../services';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {
  albumId: string = '';
  album: Album = {
    id: '',
    name: '',
    bandId: '',
    backdrop: ''
  };
  musics: Music[] = [];
  hasStyle: string = '';

  constructor(
    public activeRoute: ActivatedRoute,
    public route: Router,
    public albumService: AlbumService,
    private playlistManagerService: PlaylistManagerService
    ) { }

  ngOnInit(): void {
    this.getAlbum();
  }

  getAlbum = (): void => {
    this.activeRoute.params.subscribe(params => {
      this.albumId = params['albumId'];
    });

    this.albumService.getAlbumById(this.albumId).subscribe({
      next: (response: Album) => {
        if (response) {
          this.album = response;
          this.musics = response.musics ?? [];
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

  addToFavorites = (album: Album): void => {
    const playlist: Playlist = {
      name: album.name,
      musics: album.musics ?? []
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

  addMusicFromAlbumToPlaylist = (playlistId?: string, music?: Music ) =>{
    const playlist: Playlist = {
      id: playlistId,
      musics: [music ?? {}]
    };
    this.playlistManagerService.updatePlaylist(playlist).subscribe(() => {
      alert('Música adicionada ao favoritos!');
      this.route.navigate([`favorites/${ playlistId }`]);
    });
  }
}

