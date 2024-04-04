import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { LoginRoutingModule } from "./login.routing.module";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { LoginComponent } from "./login.component";
import { FavoritesBarModule } from "src/app/components/favorites-bar/favorites-bar.module";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatToolbarModule } from "@angular/material/toolbar";
import ToolBarSecondaryModule from "src/app/components/tool-bar-secondary/tool-bar-secondary.module";

@NgModule({
  declarations: [LoginComponent],
  imports: [LoginRoutingModule, CommonModule, MatToolbarModule,  MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatButtonModule, FavoritesBarModule, FlexLayoutModule, ToolBarSecondaryModule ],
  exports: [LoginComponent]
})
export class LoginModule { }
