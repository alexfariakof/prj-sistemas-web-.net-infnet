import { TestBed } from '@angular/core/testing';
import { Playlist } from '../../model';
import { PlaylistCacheService } from './myplaylist.cache.service';

describe('PlaylistCacheService', () => {
  let service: PlaylistCacheService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlaylistCacheService);
  });

  it('should be created', () => {
    // Assert
    expect(service).toBeTruthy();
  });

  it('should set and get playlist cache', () => {
    // Arrange
    const playlists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];

    // Act
    service.setPlaylistCache(playlists);
    const cachedPlaylists = service.getPlaylistCache();

    // Assert
    expect(cachedPlaylists).toEqual(playlists);
  });

  it('should add new playlist to cache', () => {
    // Arrange
    const newPlaylist: Playlist = { id: '3', name: 'Playlist 3', musics: [], backdrop: 'http://backdrop3.jpg' };

    // Act
    service.addToCache(newPlaylist);
    const updatedPlaylists = service.getPlaylistCache();

    // Assert
    expect(updatedPlaylists?.length).toBe(1);
    expect(updatedPlaylists).toContain(newPlaylist);
  });

  it('should update existing playlist in cache', () => {
    // Arrange
    const initialPlaylists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' }
    ];
    const updatedPlaylist: Playlist = { id: '1', name: 'Updated Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' };

    // Act
    service.setPlaylistCache(initialPlaylists);
    service.updateCache(updatedPlaylist);
    const updatedPlaylists = service.getPlaylistCache();

    // Assert
    expect(updatedPlaylists?.length).toBe(initialPlaylists.length);
    expect(updatedPlaylists).toContain(updatedPlaylist);
  });

  it('should remove playlist from cache', () => {
    // Arrange
    const playlists: Playlist[] = [
      { id: '1', name: 'Playlist 1', musics: [], backdrop: 'http://backdrop1.jpg' },
      { id: '2', name: 'Playlist 2', musics: [], backdrop: 'http://backdrop2.jpg' },
      { id: '3', name: 'Playlist 3', musics: [], backdrop: 'http://backdrop3.jpg' }
    ];
    const playlistIdToRemove = '2';

    // Act
    service.setPlaylistCache(playlists);
    service.removeFromCache(playlistIdToRemove);
    const updatedPlaylists = service.getPlaylistCache();

    // Assert
    expect(updatedPlaylists?.length).toBe(playlists.length - 1);
    expect(updatedPlaylists?.find(playlist => playlist.id === playlistIdToRemove)).toBeFalsy();
  });
});
