import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BandComponent } from './pages/band/band.component';
import { CustomerComponent } from './pages/account/customer/customer.component';
import { MerchantComponent } from './pages/account/merchant/merchant.component';
import { AlbumComponent } from './pages/album/album.component';
import { MusicComponent } from './pages/musics/musics.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
    { path: '',  pathMatch: 'full', redirectTo: '' },
    { path: '', loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) },
    { path: "login", component: LoginComponent},
    { path: "account/customer", component: CustomerComponent},
    { path: "account/merchant", component: MerchantComponent},
    { path: "favorites", loadChildren: () => import('./pages/myplaylist/myplaylist.module').then(m => m.MyplaylistModule)},
    { path: 'band', component: BandComponent},
    { path: 'band/:bandId', component: BandComponent},
    { path: 'album', component: AlbumComponent},
    { path: 'album/:albumId', component: AlbumComponent},
    { path: 'music', component: MusicComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
