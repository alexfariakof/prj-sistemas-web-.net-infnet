import { NgModule } from "@angular/core";
import { MyplaylistComponent } from "./myplaylist.component";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { SharedModule } from "src/app/components/shared.module";
import { MyplaylistRoutingModule } from "./myplaylist.routing.module";
import { FlexLayoutModule } from "@angular/flex-layout";
@NgModule({
  declarations: [MyplaylistComponent],
  imports: [MyplaylistRoutingModule, FlexLayoutModule, MatCardModule, MatButtonModule, SharedModule],
  exports: [MyplaylistComponent]
})

export class MyplaylistModule { }
