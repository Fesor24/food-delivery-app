import { Component, OnInit } from '@angular/core';
import { RestaurantService } from './restaurant/restaurant.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Glover';

  constructor(
    private restaurantService: RestaurantService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.initializeBasket();
    this.loadCurrentUser()
  }

  initializeBasket() {
    var shoppingCartId = localStorage.getItem('cart_id');

    if (shoppingCartId) {
      this.restaurantService.getShoppingCart(shoppingCartId).subscribe(
        () => {
          console.log('Cart initialized');
        },
        (error) => console.log(error)
      );
    }
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');

    this.accountService.loadCurrentUser(token).subscribe((response) => {
      console.log('User login persisted');
    });
  }
}
