import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantCircComponent } from './restaurant-circ.component';



@NgModule({
  declarations: [
    RestaurantCircComponent
  ],
  imports: [
    CommonModule
  ],
  exports : [
    RestaurantCircComponent
  ]
})
export class SharedModule { }
