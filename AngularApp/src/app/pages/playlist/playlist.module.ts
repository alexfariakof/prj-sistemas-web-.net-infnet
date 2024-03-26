import { NgModule } from "@angular/core";

import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { SharedModule } from "../../components/shared.module";
import { FlexLayoutModule } from "@angular/flex-layout";
import { PlaylistComponent } from "./playlist.component";
@NgModule({
  declarations: [PlaylistComponent],
  imports: [ FlexLayoutModule, MatCardModule, MatButtonModule, SharedModule],
  exports: [PlaylistComponent]
})

export class PlaylistModule { }
