import { Component, Input, OnInit } from '@angular/core';
import { IProducts } from 'src/app/shared/models/product';
import { RestaurantService } from '../restaurant.service';

@Component({
  selector: 'app-food-item',
  templateUrl: './food-item.component.html',
  styleUrls: ['./food-item.component.css'],
})
export class FoodItemComponent implements OnInit {
  @Input() food!: IProducts;

  constructor(private restaurantService: RestaurantService){}

  ngOnInit(): void {
  }

  addItemToBasket(){
    this.restaurantService.addItemToCart(this.food);
  }
}
