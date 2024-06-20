import { TestBed } from '@angular/core/testing';
import { AudioService } from '../../services';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('Unit Test AudioService', () => {
  let service: AudioService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [provideHttpClient(withInterceptorsFromDi())]
});
    service = TestBed.inject(AudioService);
  });

  it('should be created', () => {
    // Assert
    expect(service).toBeTruthy();
  });

  it('should play audio', () => {
    // Arrange
    const audioMock = document.createElement('audio');
    spyOn(service, 'play');

    // Act
    service.currentAudio = audioMock;
    service.play(audioMock);

    // Assert
    expect(service.play).toHaveBeenCalled();
  });

  it('should pause audio', () => {
    // Arrange
    const audioMock = document.createElement('audio');
    spyOn(service, 'pause');

    // Act
    service.pause(audioMock);

    // Assert
    expect(service.pause).toHaveBeenCalled();
  });

  it('should pause current audio when a new audio is played', () => {
    // Arrange
    let currentAudioPlay: any;
    const audioMock1 = document.createElement('audio');
    const audioMock2 = document.createElement('audio');
    spyOn(service, 'play').and.callFake(():any => {
      service.currentAudio = currentAudioPlay;
      service.pause(currentAudioPlay);
    });
    spyOn(service, 'pause').and.callThrough();

    // Act
    currentAudioPlay = audioMock1;
    service.play(currentAudioPlay);
    service.play(audioMock2);

    // Assert
    expect(service.pause).toHaveBeenCalled();
  });
});
