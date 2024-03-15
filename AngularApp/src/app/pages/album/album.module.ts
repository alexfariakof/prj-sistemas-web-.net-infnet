import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { AlbumComponent } from "./album.component";
import { AddFavoritesModule } from "src/app/components/add-favorites/add-favorites.module";
import { FavoritesBarModule } from "src/app/components/favorites-bar/favorites-bar.module";

@NgModule({
  declarations: [AlbumComponent],
  imports: [MatCardModule, MatButtonModule, CommonModule, FlexLayoutModule, FavoritesBarModule, AddFavoritesModule],
  exports: [AlbumComponent]
})

export class AlbumModule { }
