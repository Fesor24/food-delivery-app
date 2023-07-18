import { Component, Input, OnInit } from '@angular/core';
import { IShoppingCartItem } from 'src/app/shared/models/shoppingCart';
import { RestaurantService } from '../restaurant.service';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css'],
})
export class CartItemComponent implements OnInit {
  @Input() cartItem!: IShoppingCartItem;

  constructor(private restaurantService: RestaurantService){}

  ngOnInit(): void {
  }

  incrementCartItemQuantity() {
    this.restaurantService.incrementCartItemQuantity(this.cartItem);
  }

  decrementCartItemQuantity(){
    this.restaurantService.decrementCartItemQuantity(this.cartItem);
  }
}
