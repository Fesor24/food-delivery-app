import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-restaurant-circ',
  templateUrl: './restaurant-circ.component.html',
  styleUrls: ['./restaurant-circ.component.css']
})
export class RestaurantCircComponent {

@Input() restaurantName: string = "";

@Input() imageUrl: string = "";

}
