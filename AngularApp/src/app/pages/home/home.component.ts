import { Component, OnInit } from '@angular/core';
import { Playlist } from 'src/app/model';
import { PlaylistManagerService, PlaylistService } from 'src/app/services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  bands: [] = [];
  hasStyle: string = '';
  playlist: Playlist[] = [];

  constructor(public playlistService: PlaylistService, private playlistManagerService: PlaylistManagerService) { }

  ngOnInit() : void {
    this.getListOfPlaylist();
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
}
