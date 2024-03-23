import { Album } from '../model';
import { faker } from '@faker-js/faker';
import { MockMusic } from './mock.music';

export class MockAlbum {
  private static _instance: MockAlbum;
  private faker: typeof faker;

  private constructor() {
    this.faker = faker;
  }

  public static instance(): MockAlbum {
    if (!MockAlbum._instance) {
      MockAlbum._instance = new MockAlbum();
    }
    return MockAlbum._instance;
  }

  public getFaker(): Album {
    const mockAlbum: Album = ({
      id: faker.string.uuid(),
      name: faker.music.songName(),
      backdrop: faker.internet.url(),
      bandId: faker.string.uuid(),
      musics: MockMusic.instance().generateMusicList(2),
    });
    return mockAlbum;
  }

  public generateAlbumList(count: number): Album[] {
    const albumList: Album[] = [];
    for (let i = 0; i < count; i++) {
      const album: Album = this.getFaker();
      albumList.push(album);
    }
    return albumList;
  }
}
