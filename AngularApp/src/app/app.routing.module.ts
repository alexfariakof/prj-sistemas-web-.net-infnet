import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BandComponent } from './pages/band/band.component';
import { CustomerComponent } from './pages/account/customer/customer.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { MerchantComponent } from './pages/account/merchant/merchant.component';
import { MyplaylistComponent } from './pages/myplaylist/myplaylist.component';
import { AlbumComponent } from './pages/album/album.component';
import { MusicComponent } from './pages/musics/musics.component';

export const routes: Routes = [
    { path: '',  pathMatch: 'full', redirectTo: '' },
    { path: '', component: HomeComponent },
    { path: "login", component: LoginComponent},
    { path: "account/customer", component: CustomerComponent},
    { path: "account/merchant", component: MerchantComponent},
    { path: "favorites", component: MyplaylistComponent},
    { path: "favorites/:playlistId", component: MyplaylistComponent},
    { path: 'show/band', pathMatch: 'full',  component: BandComponent},
    { path: 'show/band/:bandId', pathMatch: 'full', component: BandComponent},
    { path: 'show/album', component: AlbumComponent},
    { path: 'show/album/:albumId', component: AlbumComponent},
    { path: 'show/music', pathMatch: 'full', component: MusicComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
