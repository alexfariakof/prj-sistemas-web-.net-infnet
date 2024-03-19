import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Album, Band, Playlist } from 'src/app/model';
import { MyPlaylistService, BandService } from 'src/app/services';
@Component({
  selector: 'app-band',
  templateUrl: './band.component.html',
  styleUrls: ['./band.component.css']
})
export class BandComponent implements OnInit {
  band: Band | any = {};
  albums: Album[] = [];
  hasStyle: string = '';
  myPlaylist: Playlist[] = [];


  constructor(
    private route: ActivatedRoute,
    public myPlaylistService: MyPlaylistService,
    public bandService: BandService,
    ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bandId = params['bandId'];
      this.getBand(bandId);
    });
  }

  getBand = (bandId: string): void => {
    this.bandService.getBandById(bandId).subscribe({
      next: (response: Band) => {
        if (response != null) {
          this.band = response;
          this.albums = response.albums ?? [];
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
}
