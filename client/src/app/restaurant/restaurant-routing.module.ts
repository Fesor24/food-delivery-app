import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RestaurantComponent } from './restaurant.component';
import { FoodDetailsComponent } from './food-details/food-details.component';

const routes: Routes = [
  {path: ':location', component: RestaurantComponent},
  {path: 'foods/:restaurantId', component: FoodDetailsComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class RestaurantRoutingModule { }
