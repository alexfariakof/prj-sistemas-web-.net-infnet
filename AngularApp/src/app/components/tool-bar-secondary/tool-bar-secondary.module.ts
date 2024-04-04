import { NgModule } from "@angular/core";
import { MatToolbarModule } from "@angular/material/toolbar";
import ToolBarSecondaryComponent from "./tool-bar-secondary.component";

@NgModule({
  declarations: [ToolBarSecondaryComponent],
  imports: [MatToolbarModule],
  exports: [ToolBarSecondaryComponent]
})

export default class ToolBarSecondaryModule {}
