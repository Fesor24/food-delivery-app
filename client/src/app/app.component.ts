import { Component, OnInit } from '@angular/core';
import { RestaurantService } from './restaurant/restaurant.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Glover';

  constructor(private restaurantService: RestaurantService){}

  ngOnInit(): void {
    this.initializeBasket();
  }

  initializeBasket(){
    var shoppingCartId = localStorage.getItem("cart_id");

    if(shoppingCartId){
      this.restaurantService.getShoppingCart(shoppingCartId).subscribe(() => {
        console.log("Cart initialized");
      }, error => console.log(error));
    }
  }
}
