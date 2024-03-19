import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Album, Music, Playlist} from 'src/app/model';
import { AlbumService, PlaylistManagerService } from 'src/app/services';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {
  album: Album = {
    id: '',
    name: '',
    bandId: '',
    backdrop: ''
  };
  musics: Music[] = [];
  hasStyle: string = '';

  constructor(
    private route: ActivatedRoute,
    public albumService: AlbumService,
    private playlistManagerService: PlaylistManagerService
    ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const albumId = params['albumId'];
      this.getAlbum(albumId);
    });
  }

  getAlbum = (albumId: string): void => {
    this.albumService.getAlbumById(albumId).subscribe({
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
    const playlist: Playlist =
    {
      name: album.name,
      musics: album.musics ?? []
    }
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

