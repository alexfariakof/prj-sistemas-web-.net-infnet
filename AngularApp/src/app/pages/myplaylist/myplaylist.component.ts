import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Music, Playlist } from '../../model';
import { MyPlaylistService, PlaylistManagerService } from '../../services';
@Component({
  selector: 'app-myplaylist',
  templateUrl: './myplaylist.component.html',
  styleUrls: ['./myplaylist.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class MyplaylistComponent implements OnInit {
  playlistId: string = '';
  hasStyle: string = '';
  myPlaylist: Playlist | any = {};
  musics: Music[] = [];

  constructor(
    public activeRoute: ActivatedRoute,
    public route: Router,
    public playlistManagerService: PlaylistManagerService,
    public myPlaylistService: MyPlaylistService) { }

  ngOnInit(): void {
    this.getMyplaylist(this.playlistId);
  }

  getMyplaylist = (playlistId: string ): void => {
    this.activeRoute.params.subscribe(params => {
      this.playlistId = params['playlistId'];
    });

    this.myPlaylistService.getPlaylist(this.playlistId).subscribe({
      next: (response: Playlist) => {
        if (response != null) {
          this.myPlaylist = response;
          this.musics = response.musics;
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

  addMusicToFavorites = (playlistId?: string, music?: Music ) => {
    const playlist: Playlist = {
      id: playlistId,
      musics: [music ?? {}]
    };
    this.playlistManagerService.updatePlaylist(playlist).subscribe(() => {
      alert('Música adicionada ao favoritos!');
      this.route.navigate([`favorites/${ playlistId }`]);
    });
  }

  removeMusicFromFavotites = (music: Music) => {
    this.myPlaylistService.removeMusicFromFavotites(this.myPlaylist.id, music.id).subscribe(() => {
      alert('Música removida com sucesso!');
      this.getMyplaylist(this.playlistId);
    });
  }

  trackMusics(index: number, music: Music): string {
    return music.id as string;
  }
}
