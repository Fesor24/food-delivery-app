import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantCircComponent } from './components/restaurant-circ/restaurant-circ.component';
import { ReactiveFormsModule } from '@angular/forms';




@NgModule({
  declarations: [
    RestaurantCircComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports : [
    RestaurantCircComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
