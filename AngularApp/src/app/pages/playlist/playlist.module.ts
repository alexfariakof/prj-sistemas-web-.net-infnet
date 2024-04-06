import { NgModule } from "@angular/core";
import { SharedModule } from "../../components/shared.module";
import { PlaylistComponent } from "./playlist.component";
@NgModule({
  declarations: [PlaylistComponent],
  imports: [SharedModule],
  exports: [PlaylistComponent]
})

export class PlaylistModule { }
