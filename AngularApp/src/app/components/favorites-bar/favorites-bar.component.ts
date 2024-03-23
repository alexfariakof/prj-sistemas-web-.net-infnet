import { Component, OnInit } from '@angular/core';
import { Playlist } from 'src/app/model';
import { PlaylistManagerService } from '../../services';

@Component({
  selector: 'app-favorites-bar',
  templateUrl: './favorites-bar.component.html',
  styleUrls: ['./favorites-bar.component.css']
})
export class FavoritesBarComponent implements OnInit {
  myPlaylist: Playlist[] = [];
  isCreatingPlaylist: boolean = false;
  newPlaylistName: string = '';

  constructor(private playlistManagerService: PlaylistManagerService) { }

  ngOnInit(): void {
    this.initializePlaylist();
  }

  initializePlaylist = (): void =>{
    this.playlistManagerService.playlists$.subscribe(playlists => {
      this.myPlaylist = playlists;
    });
  }

  toggleCreatePlaylist(): void {
    this.isCreatingPlaylist = !this.isCreatingPlaylist;
    if (!this.isCreatingPlaylist && this.newPlaylistName == '') {
      this.newPlaylistName = '';
    }
  }

  createPlaylist(): void {
    if (this.newPlaylistName.trim() !== '') {
      const playlist: Playlist = {
        name: this.newPlaylistName,
        musics: []
      };
      this.playlistManagerService.createPlaylist(playlist).subscribe(() => {
        this.newPlaylistName = '';
        this.isCreatingPlaylist = false;
        this.initializePlaylist();
      });
    }
  }

  removePlaylist(playlistId?: string): void {
    this.playlistManagerService.deletePlaylist(playlistId ?? '').subscribe(playlists => {
      this.initializePlaylist();
      alert('Playlist exluída com sucesso!')
    });
  }
}
