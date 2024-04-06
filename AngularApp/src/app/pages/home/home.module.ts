import { NgModule } from "@angular/core";
import { HomeComponent } from "./home.component";
import { SharedModule } from "../../components/shared.module";
import { HomeRoutingModule } from "./home.routing.module";

@NgModule({
  declarations: [HomeComponent],
  imports: [HomeRoutingModule, SharedModule ],
  exports: [HomeComponent]
})

export class HomeModule { }
