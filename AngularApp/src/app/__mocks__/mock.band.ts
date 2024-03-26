import { Band } from '../model';
import { faker } from '@faker-js/faker';
import { MockMusic } from './mock.music';

export class MockBand {
  private static _instance: MockBand;
  private faker: typeof faker;

  private constructor() {
    this.faker = faker;
  }

  public static instance(): MockBand {
    if (!MockBand._instance) {
      MockBand._instance = new MockBand();
    }
    return MockBand._instance;
  }

  public getFaker(): Band {
    const mockBand: Band = ({
      id: faker.string.uuid(),
      name: faker.music.songName(),
      description: faker.lorem.words(),
      backdrop: faker.internet.url(),
      albums: Array.from({ length: 5 }, () => ({
        id: faker.string.uuid(),
        name: faker.music.songName(),
        backdrop: faker.internet.url(),
        flatId: faker.string.uuid(),
        bandId: faker.string.uuid(),
        musics: MockMusic.instance().generateMusicList(2)
      })),
    });
    return mockBand;
  }

  public generateBandList(count: number): Band[] {
    const BandList: Band[] = [];
    for (let i = 0; i < count; i++) {
      const Band: Band = this.getFaker();
      BandList.push(Band);
    }
    return BandList;
  }
}
