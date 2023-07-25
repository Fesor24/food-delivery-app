import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { OrderItemComponent } from './order-item/order-item.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    CheckoutComponent,
    OrderItemComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule
  ]
})
export class CheckoutModule { }
