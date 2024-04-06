import { NgModule } from "@angular/core";
import { LoginRoutingModule } from "./login.routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { LoginComponent } from "./login.component";
import { FlexLayoutModule } from "@angular/flex-layout";
import ToolBarSecondaryModule from "src/app/components/tool-bar-secondary/tool-bar-secondary.module";
import { MatInputModule } from "@angular/material/input";

@NgModule({
  declarations: [LoginComponent],
  imports: [LoginRoutingModule, MatInputModule, ReactiveFormsModule, FlexLayoutModule, ToolBarSecondaryModule ],
  exports: [LoginComponent]
})
export class LoginModule { }
