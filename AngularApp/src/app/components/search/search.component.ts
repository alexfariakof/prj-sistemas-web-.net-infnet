import { Component } from '@angular/core';
import { Music } from '../../model';
import { MusicService } from '../../services';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})

export class SearchComponent {
  search: string = '';
  musics: Music[] = [];

  constructor(public musicService: MusicService) {}

  searchMusic = (event: any): void => {
    this.musicService.searchMusic(this.search).subscribe({
      next: (response: Music[]) => {
        if (response) {
          this.musics = response ?? [];
        }
        else {
          throw (response);
        }
      },
      error: (response: any) => {
        console.log(response.error);
      },
      complete() { }
    });
  }

  clearPesquisar(): void {
    this.search = '';
    this.musics = [];
  }
  trackByMusic(index: number, music: Music): Music {
    return music;
  }
}
