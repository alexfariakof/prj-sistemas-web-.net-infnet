import { Component, OnInit } from '@angular/core';
import { map, catchError } from 'rxjs';
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

  ngOnInit(): void {
    this.getListOfMyplaylist();
  }

  getListOfMyplaylist = (): void => {
    this.myPlaylistService.getAllPlaylist()
    .pipe(
      map((response: Playlist[]) => {
        if (response ?? response) {
          return response;
        }
        else {
          throw (response);
        }
      }),
      catchError((error) => {
        throw (error);
      })
    )    
    .subscribe({
      next: (response: Playlist[]) => {
        if (response != null) 
          this.myPlaylist = response;
      },
      error: (response: any) => {
        console.log(response.error);
      },
      complete() { }
    });
  }
}