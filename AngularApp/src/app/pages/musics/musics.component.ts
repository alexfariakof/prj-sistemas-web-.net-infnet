import { PlaylistManagerService } from './../../services/myplaylist/myplaylist.manager.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Music, Playlist } from 'src/app/model';
import { AlbumService, BandService, MusicService } from 'src/app/services';

@Component({
  selector: 'app-musics',
  templateUrl: './musics.component.html',
  styleUrls: ['./musics.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class MusicComponent implements OnInit {
  musicId: string = '';
  music: Music = {};
  hasStyle: string = '';

  constructor(
    public activeRoute: ActivatedRoute,
    public route: Router,
    public musicService: MusicService,
    public albumService: AlbumService,
    public bandService: BandService,
    public playlistManagerService: PlaylistManagerService

  ) { }

  ngOnInit(): void {
    this.activeRoute.params.subscribe(params => {
      this.musicId = params['musicId'];
    });

    this.musicService.getMusicById(this.musicId).subscribe({
      next: (response: Music) => {
        if (response) {
          this.music = response;
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

  addMusicToPlaylist = (playlistId?: string, music?: Music ) =>{
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
