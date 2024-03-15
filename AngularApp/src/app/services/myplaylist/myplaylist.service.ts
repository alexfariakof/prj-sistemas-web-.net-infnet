import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Playlist } from '../../model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MyPlaylistService {
  public routeUrl:string = 'customer/myplaylist';

  constructor(public httpClient: HttpClient) { }

  public getAllPlaylist(): Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(`${ this.routeUrl }`);
  }

  public getPlaylist(playlistId: string): Observable<Playlist> {
    return this.httpClient.get<Playlist>(`${ this.routeUrl }/${playlistId}`);
  }
}
