import { Music } from '../model';
import { faker } from '@faker-js/faker';

export class MockMusic {
  private static _instance: MockMusic;
  private faker: typeof faker;

  private constructor() {
    this.faker = faker;
  }

  public static instance(): MockMusic {
    if (!MockMusic._instance) {
      MockMusic._instance = new MockMusic();
    }
    return MockMusic._instance;
  }

  public getFaker(): Music {
    const mockMusic: Music = ({
      id: faker.string.uuid(),
      name: faker.music.songName(),
      duration: faker.number.int(),
      url: faker.internet.url(),
    });
    return mockMusic;
  }

  public generateMusicList(count: number): Music[] {
    const MusicList: Music[] = [];
    for (let i = 0; i < count; i++) {
      const Music: Music = this.getFaker();
      MusicList.push(Music);
    }
    return MusicList;
  }
}
