import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { BandComponent } from "./band.component";
import { SharedModule } from "../../components/shared.module";
import { FlexLayoutModule } from "@angular/flex-layout";
@NgModule({
  declarations: [BandComponent],
  imports: [MatCardModule, MatButtonModule, FlexLayoutModule, SharedModule],
  exports: [BandComponent]
})

export class BandModule { }
