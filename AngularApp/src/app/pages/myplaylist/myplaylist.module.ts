import { NgModule } from "@angular/core";
import { MyplaylistComponent } from "./myplaylist.component";
import { CommonModule } from "@angular/common";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { FavoritesBarModule } from "src/app/components/favorites-bar/favorites-bar.module";

@NgModule({
  declarations: [MyplaylistComponent],
  imports: [MatCardModule, MatButtonModule, CommonModule, FlexLayoutModule, FavoritesBarModule],
  exports: [MyplaylistComponent]
})

export class MyplaylistModule { }
