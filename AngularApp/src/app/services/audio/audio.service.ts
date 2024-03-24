import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AudioService {
  private currentAudio: HTMLAudioElement | null = null;

  play(audio: HTMLAudioElement): void {
    if (this.currentAudio && this.currentAudio !== audio) {
      this.currentAudio.pause(); // Pausa o áudio atual se houver outro diferente sendo reproduzido
    }
    this.currentAudio = audio;
    audio.play();
  }

  pause(audio: HTMLAudioElement): void {
    audio.pause();
  }
}
