import { NgModule } from "@angular/core";
import { MusicComponent } from "./musics.component";
import { SharedModule } from "../../components/shared.module";

@NgModule({
  declarations: [MusicComponent],
  imports: [ SharedModule],
  exports: [MusicComponent]
})

export class MusicModule { }
