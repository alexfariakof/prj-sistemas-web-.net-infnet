import { NgModule } from "@angular/core";
import { MyplaylistComponent } from "./myplaylist.component";
import { SharedModule } from "../../components/shared.module";
@NgModule({
  declarations: [MyplaylistComponent],
  imports: [SharedModule],
  exports: [MyplaylistComponent]
})

export class MyplaylistModule { }
