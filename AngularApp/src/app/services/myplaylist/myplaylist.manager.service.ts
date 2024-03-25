import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Playlist } from '../../model';
import { PlaylistCacheService } from './myplaylist.cache.service';
import { MyPlaylistService } from './myplaylist.service';

@Injectable({
  providedIn: 'root'
})
export class PlaylistManagerService {
  private playlistsSubject = new Subject<Playlist[]>();
  playlists$ = this.playlistsSubject.asObservable();

  constructor(
    private myPlaylistService: MyPlaylistService,
    private playlistCacheService: PlaylistCacheService
  ) {
    this.getListOfPlaylists().subscribe();
  }

  getCachedPlaylist(): Playlist[] {
    const cachedPlaylists = this.playlistCacheService.getPlaylistCache();
    if (cachedPlaylists) {
      return cachedPlaylists;
    }
    return [];
  }

  getListOfPlaylists(): Observable<Playlist[]> {
    const cachedPlaylists = this.playlistCacheService.getPlaylistCache();
    if (cachedPlaylists) {
      this.playlistsSubject.next(cachedPlaylists);
    }

    return this.myPlaylistService.getAllPlaylist().pipe(
      tap(playlists => {
        this.playlistCacheService.setPlaylistCache(playlists);
        this.playlistsSubject.next(playlists);
      }),
      catchError(error => {
        this.playlistsSubject.error(error);
        return [];
      })
    );
  }

  createPlaylist(newPlaylist: Playlist): Observable<Playlist> {
    return this.myPlaylistService.createPlaylist(newPlaylist).pipe(
      tap(playlist => {
        const updatedPlaylists = this.playlistCacheService.addToCache(playlist);
        this.playlistsSubject.next(updatedPlaylists);
      }),
      catchError(error => {
        this.playlistsSubject.error(error);
        return [];
      })
    );
  }

  updatePlaylist(updatedPlaylist: Playlist): Observable<Playlist> {
    return this.myPlaylistService.updatePlaylist(updatedPlaylist).pipe(
      tap(playlist => {
        const updatedPlaylists = this.playlistCacheService.updateCache(playlist);
        this.playlistsSubject.next(updatedPlaylists);
      }),
      catchError(error => {
        this.playlistsSubject.error(error);
        return [];
      })
    );
  }

  deletePlaylist(playlistId: string): Observable<any> {
    return this.myPlaylistService.deletePlaylist(playlistId).pipe(
      tap((result: boolean) => {
        if (result) {
          const updatedPlaylists = this.playlistCacheService.removeFromCache(playlistId);
          this.playlistsSubject.next(updatedPlaylists);
        } else {
          console.error('Erro ao deletar playlist.');
        }
      }),
      catchError(error => {
        this.playlistsSubject.error(error);
        return of(false);
      })
    );
  }
}
