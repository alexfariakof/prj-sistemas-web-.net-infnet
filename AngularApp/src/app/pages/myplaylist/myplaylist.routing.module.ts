import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyplaylistComponent } from './myplaylist.component';

const routes: Routes = [
  { path: '', component: MyplaylistComponent },
  { path: ":playlistId", component: MyplaylistComponent }
];
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class MyplaylistRoutingModule{}