import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AudioService } from '../../services/audio/audio.service';
import { AudioPlayerComponent } from './audio-player.component';

describe('Unit Test AudioPlayerComponent', () => {
  let component: AudioPlayerComponent;
  let fixture: ComponentFixture<AudioPlayerComponent>;
  let audioService: AudioService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AudioPlayerComponent],
      providers: [AudioService]
    });
    fixture = TestBed.createComponent(AudioPlayerComponent);
    component = fixture.componentInstance;
    audioService = TestBed.inject(AudioService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should call play method of AudioService on onPlay', () => {
    // Arrange
    const audioMock = document.createElement('audio');
    spyOn(audioService, 'play').and.callFake(():any => {});

    // Act
    component.audioElement = audioMock;
    component.onPlay();

    // Assert
    expect(audioService.play).toHaveBeenCalled();
  });
});
