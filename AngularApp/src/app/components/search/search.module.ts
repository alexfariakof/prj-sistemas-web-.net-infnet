import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { FormsModule } from "@angular/forms";
import { SearchComponent } from "./search.component";

@NgModule({
  declarations: [SearchComponent ],
  imports: [MatCardModule, MatButtonModule, CommonModule, FormsModule, FlexLayoutModule],
  exports: [SearchComponent]
})

export class SearchModule { }
