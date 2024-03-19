import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Playlist } from 'src/app/model';

@Injectable({
  providedIn: 'root'
})
export class PlaylistCacheService {
  private playlistCacheSubject = new BehaviorSubject<Playlist[] | null>(null);
  playlistCache$: Observable<Playlist[] | null> = this.playlistCacheSubject.asObservable();

  constructor() {}

  setPlaylistCache(playlists: Playlist[]): void {
    this.playlistCacheSubject.next(playlists);
  }

  getPlaylistCache(): Playlist[] | null {
    return this.playlistCacheSubject.getValue();
  }

  addToCache(newPlaylist: Playlist): Playlist[] {
    const currentPlaylists = this.getPlaylistCache() || [];
    const updatedPlaylists = [...currentPlaylists, newPlaylist];
    this.setPlaylistCache(updatedPlaylists);
    return updatedPlaylists;
  }

  updateCache(updatedPlaylist: Playlist): Playlist[] {
    const currentPlaylists = this.getPlaylistCache() || [];
    const index = currentPlaylists.findIndex(playlist => playlist.id === updatedPlaylist.id);
    if (index !== -1) {
      currentPlaylists[index] = updatedPlaylist;
      this.setPlaylistCache(currentPlaylists);
    }
    return currentPlaylists;
  }

  removeFromCache(playlistId: string): Playlist[] {
    const currentPlaylists = this.getPlaylistCache() || [];
    const updatedPlaylists = currentPlaylists.filter(playlist => playlist.id !== playlistId);
    this.setPlaylistCache(updatedPlaylists);
    return updatedPlaylists;
  }
}
