import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-restaurant-rec',
  templateUrl: './restaurant-rec.component.html',
  styleUrls: ['./restaurant-rec.component.css']
})
export class RestaurantRecComponent {

  @Input() name!: string;

  @Input() image!: string;

  @Input() ratings!: number;

  @Input() reviews!: number;

  @Input() restaurantId!:string;

}
