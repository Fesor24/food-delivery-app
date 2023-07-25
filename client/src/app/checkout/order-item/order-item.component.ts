import { Component, Input } from '@angular/core';
import { RestaurantService } from 'src/app/restaurant/restaurant.service';
import { IShoppingCartItem } from 'src/app/shared/models/shoppingCart';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent {

  @Input() orderItem! : IShoppingCartItem

  constructor(private restaurantService: RestaurantService){}

addItemToBasket(){
  this.restaurantService.incrementCartItemQuantity(this.orderItem)
}

decreaseCartItemQuantity(){
  this.restaurantService.decrementCartItemQuantity(this.orderItem);
}

}
