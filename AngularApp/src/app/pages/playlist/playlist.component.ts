import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Music, Playlist } from 'src/app/model';
import { PlaylistService } from '../../services';
@Component({
  selector: 'app-playlist',
  templateUrl: './playlist.component.html',
  styleUrls: ['./playlist.component.css']
})
export class PlaylistComponent implements OnInit {
  hasStyle: string = '';
  playlist: Playlist | any = {};
  musics: Music[] = [];

  constructor(
    private route: ActivatedRoute,
    public playlistService: PlaylistService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const playlistId = params['playlistId'];
        this.getplaylist(playlistId);
    });
  }

  getplaylist = (playlistId: string ): void => {
    this.playlistService.getPlaylistById(playlistId).subscribe({
      next: (response: Playlist) => {
        if (response ?? {}) {
          this.playlist = response;
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
}
