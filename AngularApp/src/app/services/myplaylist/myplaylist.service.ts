import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Playlist } from '../../model';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MyPlaylistService {
  private routeUrl = 'api/customer/myplaylist';

  constructor(public httpClient: HttpClient) { }

  public getAllPlaylist(): Observable<Playlist[]> {
    return this.httpClient.get<Playlist[]>(this.routeUrl).pipe(
      catchError(error => {
        return throwError(() => error);
      })
    );
  }

  public getPlaylist(playlistId: string): Observable<Playlist> {
    return this.httpClient.get<Playlist>(`${this.routeUrl}/${playlistId}`).pipe(
      catchError(error => {
        return throwError(() => error);
      })
    );
  }
}
