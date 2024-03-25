import { Component, Input, ViewChild } from '@angular/core';
import { AudioService } from '../../services';

@Component({
  selector: 'app-audio-player',
  templateUrl: './audio-player.component.html',
  styleUrls: ['./audio-player.component.css']
})
export class AudioPlayerComponent {
  @Input() audioUrl?: string;
  @ViewChild('audioElement') audioElement: any;

  constructor(private audioService: AudioService) {}

  onPlay(): void {
    const audio: HTMLAudioElement = this.audioElement.nativeElement;
    this.audioService.play(audio);
  }
}
