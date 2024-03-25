import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AudioService {
  public currentAudio: HTMLAudioElement | null = null;

  play(audio: HTMLAudioElement): void {
    if (this.currentAudio && this.currentAudio !== audio) {
      this.currentAudio.pause();
    }
    this.currentAudio = audio;
    audio.play();
  }

  pause(audio: HTMLAudioElement): void {
    audio.pause();
  }
}
