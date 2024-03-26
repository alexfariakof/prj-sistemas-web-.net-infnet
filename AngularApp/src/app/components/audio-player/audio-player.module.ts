import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AudioPlayerComponent } from "./audio-player.component";

@NgModule({
  declarations: [AudioPlayerComponent ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  exports: [AudioPlayerComponent]
})

export class AudioPlayerModule { }
