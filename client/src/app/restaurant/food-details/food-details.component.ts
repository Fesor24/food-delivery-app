import { Component, OnInit, HostListener } from '@angular/core';
import { RestaurantService } from '../restaurant.service';
import { ActivatedRoute } from '@angular/router';
import { IProducts } from 'src/app/shared/models/product';
import { IShoppingCart, IShoppingCartItem, IShoppingCartTotals } from 'src/app/shared/models/shoppingCart';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-food-details',
  templateUrl: './food-details.component.html',
  styleUrls: ['./food-details.component.css'],
})
export class FoodDetailsComponent implements OnInit {
  constructor(
    private restaurantService: RestaurantService,
    private activatedRoute: ActivatedRoute
  ) {}

  scrolledPastPoint: boolean = false;

  products: IProducts[] = [];

  location!: string;

  cart$!: Observable<IShoppingCart | null>;

  shoppingCartTotals$!: Observable<IShoppingCartTotals>;

  ngOnInit(): void {
    this.getProductsByRestaurantId();
    let deliveryLocation = localStorage.getItem('location');

    console.log(deliveryLocation, 'delivery location');

    deliveryLocation
      ? (this.location = deliveryLocation)
      : (this.location = 'Order now');

    this.cart$ = this.restaurantService.shoppingCart$;

    this.shoppingCartTotals$ = this.restaurantService.shoppingCartTotal$

    console.log(this.cart$);
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const desiredPoint = 300;

    const scrollPosition = document.documentElement.scrollTop;

    this.scrolledPastPoint = scrollPosition > desiredPoint;
  }

  getProductsByRestaurantId() {
    let restaurantId =
      this.activatedRoute.snapshot.paramMap.get('restaurantId');

    if (restaurantId) {
      this.restaurantService
        .getFoodsByRestaurant(restaurantId)
        .subscribe((res) => {
          if (res.successful) {
            console.log('data', res.result);
            this.products = res.result;
          } else {
            console.log(res.errorMessage);
          }
        });
    }
  }


}
