import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'restaurant', loadChildren: () => import('./restaurant/restaurant.module')
  .then(mod => mod.RestaurantModule)},
  {path: 'checkout', loadChildren: ()=> import('./checkout/checkout.module').then(mod => mod.CheckoutModule)},
  {path: '**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
