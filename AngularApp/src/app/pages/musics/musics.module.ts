import { NgModule } from "@angular/core";
import { MusicComponent } from "./musics.component";
import { CommonModule } from "@angular/common";
import { MatCardModule } from "@angular/material/card";
import { SharedModule } from "src/app/components/shared.module";
import { FlexLayoutModule } from "@angular/flex-layout";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [MusicComponent],
  imports: [CommonModule,  FormsModule, ReactiveFormsModule, MatCardModule, FlexLayoutModule, SharedModule],
  exports: [MusicComponent]
})

export class MusicModule { }
