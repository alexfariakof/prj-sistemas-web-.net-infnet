import { NgModule } from "@angular/core";
import { AlbumComponent } from "./album.component";
import { SharedModule } from "../../components/shared.module";

@NgModule({
  declarations: [AlbumComponent],
  imports: [ SharedModule],
  exports: [AlbumComponent]
})

export class AlbumModule { }
