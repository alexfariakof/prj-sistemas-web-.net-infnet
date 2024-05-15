import { NgModule } from "@angular/core";
import { FlexLayoutModule } from "@angular/flex-layout";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatToolbarModule } from "@angular/material/toolbar";
import ToolBarSecondaryModule from "src/app/components/tool-bar-secondary/tool-bar-secondary.module";
import { CustomerComponent } from "./customer.component";

@NgModule({
  declarations: [CustomerComponent ],
  imports: [ CommonModule, MatToolbarModule, ToolBarSecondaryModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, FlexLayoutModule],
  exports: [CustomerComponent ]
})

export class CustomerModule { }
