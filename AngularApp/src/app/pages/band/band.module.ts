import { NgModule } from "@angular/core";
import { BandComponent } from "./band.component";
import { SharedModule } from "../../components/shared.module";
@NgModule({
  declarations: [BandComponent],
  imports: [ SharedModule],
  exports: [BandComponent]
})

export class BandModule { }
