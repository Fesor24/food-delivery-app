import { Component, OnDestroy, OnInit } from '@angular/core';
import { RestaurantService } from '../restaurant/restaurant.service';
import { IShoppingCart, IShoppingCartItem, IShoppingCartTotals } from '../shared/models/shoppingCart';
import { Observable, Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IOrder } from '../shared/models/order';
import { CheckoutService } from './checkout.service';
import { IRestaurant } from '../shared/models/restaurant';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit, OnDestroy{

  shoppingCart$! : Observable<IShoppingCart | null>;

  shoppingCartSubscription!: Subscription;

  shoppingCartCharges$! : Observable<IShoppingCartTotals | null>;

  restaurant!:IRestaurant;

  order: IOrder = {
    cartId: '',
    address: {
      firstName: '',
      lastName: '',
      state : '',
      street: '',
      city: ''
    }
  };

  addressForm!: FormGroup;

  constructor(private restaurantService: RestaurantService, private checkoutService: CheckoutService){}

  ngOnDestroy(): void {
    this.shoppingCartSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.shoppingCart$ = this.restaurantService.shoppingCart$
    this.shoppingCartCharges$ = this.restaurantService.shoppingCartTotal$
    this.createAddressForm();
    this.fetchRestaurant();
  }

  createAddressForm(){
    this.addressForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      street: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required)
    });
  }

  createOrder(){
    this.shoppingCartSubscription =  this.shoppingCart$.subscribe((data) => {
      if(data){
        this.order.cartId = data.id;
      }

    })

    this.order.address = this.addressForm.value;

    console.log(this.order);
  }

  fetchRestaurant(){
    let restaurantId = "";
    this.shoppingCartSubscription = this.shoppingCart$.subscribe((data) => {
      if(data?.items && data.items.length > 0){
        restaurantId = data.items[0].restaurantId;

        this.checkoutService.fetchRestaurant(restaurantId).subscribe((response) => {
          if(response){
            this.restaurant = response
          }
        }, error => console.log(error));
      }
    })
  }

}
