import { Playlist } from '../model';
import { faker } from '@faker-js/faker';
import { MockMusic } from './mock.music';

export class MockPlaylist {
  private static _instance: MockPlaylist;
  private faker: typeof faker;

  private constructor() {
    this.faker = faker;
  }

  public static instance(): MockPlaylist {
    if (!MockPlaylist._instance) {
      MockPlaylist._instance = new MockPlaylist();
    }
    return MockPlaylist._instance;
  }

  public getFaker(): Playlist {
    const mockPlaylist: Playlist = ({
      id: faker.string.uuid(),
      name: faker.music.songName(),
      backdrop: faker.internet.url(),
      musics: MockMusic.instance().generateMusicList(2)
    });
    return mockPlaylist;
  }

  public generatePlaylistList(count: number): Playlist[] {
    const PlaylistList: Playlist[] = [];
    for (let i = 0; i < count; i++) {
      const Playlist: Playlist = this.getFaker();
      PlaylistList.push(Playlist);
    }
    return PlaylistList;
  }
}
