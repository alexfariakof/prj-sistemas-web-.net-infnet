import { Component, OnInit } from '@angular/core';
import { Playlist } from 'src/app/model';
import { MyPlaylistService } from 'src/app/services';

@Component({
  selector: 'app-favorites-bar',
  templateUrl: './favorites-bar.component.html',
  styleUrls: ['./favorites-bar.component.css']
})
export class FavoritesBarComponent implements OnInit {
  myPlaylist: Playlist[] = [];

  constructor(public myPlaylistService: MyPlaylistService) { }

  async ngOnInit() {
    await this.getListOfMyplaylist();

  }

  getListOfMyplaylist = (): void => {
    this.myPlaylistService.getAllPlaylist().subscribe({
      next: (response: Playlist[]) => {
        if (response != null) {
          this.myPlaylist = response;
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
