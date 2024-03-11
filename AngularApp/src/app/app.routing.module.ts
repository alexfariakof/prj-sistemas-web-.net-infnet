import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { CustomerComponent } from './pages/customer/customer.component';
import { MerchantComponent } from './pages/merchant/merchant.component';

export const routes: Routes = [
    { path: '',  pathMatch: 'full', redirectTo: 'home' },
    { path: 'home', component: HomeComponent },
    { path: "login", component: LoginComponent},
    { path: "cliente", component: CustomerComponent},
    { path: "comerciante", component: MerchantComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
