import { NgModule } from "@angular/core";
import { AddFavoritesComponent } from "./add-favorites/add-favorites.component";
import { FavoritesBarComponent } from "./favorites-bar/favorites-bar.component";
import { CommonModule } from "@angular/common";
import { AddFavoritesModule } from "./add-favorites/add-favorites.module";
import { FavoritesBarModule } from "./favorites-bar/favorites-bar.module";
import { AudioPlayerModule } from "./audio-player/audio-player.module";
import { AudioPlayerComponent } from "./audio-player/audio-player.component";
import { SearchModule } from "./search/search.module";
import { SearchComponent } from "./search/search.component";
import { MasterPageComponent } from './master.page/master.page.component';
import { MasterPageModule } from "./master.page/master.page.module";
import { MatCardModule } from "@angular/material/card";
import { FlexLayoutModule } from "@angular/flex-layout";

@NgModule({
  imports: [FlexLayoutModule, CommonModule, MatCardModule, MasterPageModule,  AddFavoritesModule, FavoritesBarModule, AudioPlayerModule, SearchModule ],
  exports: [CommonModule, FlexLayoutModule, MatCardModule, MasterPageComponent, AddFavoritesComponent, FavoritesBarComponent, AudioPlayerComponent, SearchComponent]
})

export class SharedModule { }
