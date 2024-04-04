import { NgModule } from "@angular/core";
import { MasterPageComponent } from "./master.page.component";
import { ToolBarModule } from "../tool-bar/tool-bar.module";
import { FavoritesBarModule } from "../favorites-bar/favorites-bar.module";
import { FlexLayoutModule } from "@angular/flex-layout";

@NgModule({
  declarations: [MasterPageComponent ],
  imports: [ToolBarModule, FavoritesBarModule, FlexLayoutModule],
  exports: [MasterPageComponent]
})

export class MasterPageModule { }
