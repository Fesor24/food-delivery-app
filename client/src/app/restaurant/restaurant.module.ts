import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantComponent } from './restaurant.component';
import { FoodDetailsComponent } from './food-details/food-details.component';
import { RestaurantRoutingModule } from './restaurant-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { RestaurantNavComponent } from './restaurant-nav/restaurant-nav.component';
import { DeliveryItemsComponent } from './delivery-items/delivery-items.component';
import { RestaurantRecComponent } from './restaurant-rec/restaurant-rec.component';



@NgModule({
  declarations: [
    RestaurantComponent,
    FoodDetailsComponent,
    RestaurantNavComponent,
    DeliveryItemsComponent,
    RestaurantRecComponent,
  ],
  imports: [
    CommonModule,
    RestaurantRoutingModule,
    HttpClientModule
  ],
})
export class RestaurantModule {}
