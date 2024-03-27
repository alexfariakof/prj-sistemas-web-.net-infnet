import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { ToolBarComponent } from "./tool-bar.component";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatButtonModule } from "@angular/material/button";
import { FormsModule } from "@angular/forms";
import { SearchModule } from "../search/search.module";

@NgModule({
  declarations: [ToolBarComponent],
  imports: [CommonModule, FormsModule, FlexLayoutModule, MatToolbarModule, MatButtonModule, SearchModule],
  exports: [ToolBarComponent]
})

export class ToolBarModule { }
