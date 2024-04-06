import { NgModule } from "@angular/core";
import { MusicComponent } from "./musics.component";
import { SharedModule } from "src/app/components/shared.module";
@NgModule({
  declarations: [MusicComponent],
  imports: [ SharedModule],
  exports: [MusicComponent]
})

export class MusicModule { }
