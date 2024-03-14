import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { MerchantComponent } from './pages/account/merchant/merchant.component';
import { MyplaylistComponent } from './pages/myplaylist/myplaylist.component';
import { CustomerComponent } from './pages/account/customer/customer.component';

export const routes: Routes = [
    { path: '',  pathMatch: 'full', redirectTo: '' },
    { path: '', component: HomeComponent },
    { path: "login", component: LoginComponent},
    { path: "account/customer", component: CustomerComponent},
    { path: "account/merchant", component: MerchantComponent},
    { path: "myplaylist", component: MyplaylistComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
