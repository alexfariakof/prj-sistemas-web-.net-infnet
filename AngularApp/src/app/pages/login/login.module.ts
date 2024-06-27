import { NgModule } from "@angular/core";
import { LoginRoutingModule } from "./login.routing.module";
import { ReactiveFormsModule } from "@angular/forms";
import { LoginComponent } from "./login.component";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatInputModule } from "@angular/material/input";
import ToolBarSecondaryModule from "../../components/tool-bar-secondary/tool-bar-secondary.module";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";

@NgModule({
  declarations: [LoginComponent],
  imports: [CommonModule, LoginRoutingModule, MatInputModule, MatIconModule, ReactiveFormsModule, FlexLayoutModule, ToolBarSecondaryModule ],
  exports: [LoginComponent]
})
export class LoginModule { }
