import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantCircComponent } from './components/restaurant-circ/restaurant-circ.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './components/text-input/text-input.component';




@NgModule({
  declarations: [
    RestaurantCircComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports : [
    RestaurantCircComponent,
    ReactiveFormsModule,
    TextInputComponent
  ]
})
export class SharedModule { }
