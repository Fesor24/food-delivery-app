import { Component, Input } from '@angular/core';
import { IProducts } from 'src/app/shared/models/product';

@Component({
  selector: 'app-food-item',
  templateUrl: './food-item.component.html',
  styleUrls: ['./food-item.component.css'],
})
export class FoodItemComponent {

  @Input() food!: IProducts;
}
