import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { FavoritesBarComponent } from "./favorites-bar.component";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [FavoritesBarComponent ],
  imports: [MatCardModule, MatButtonModule, CommonModule, FormsModule, FlexLayoutModule],
  exports: [FavoritesBarComponent]
})

export class FavoritesBarModule { }
