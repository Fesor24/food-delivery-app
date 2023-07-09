import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from './restaurant.service';
import { IRestaurant } from '../shared/models/restaurant';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnInit{

  location!: string

  restaurants: IRestaurant[] = [];

  constructor(private activatedRoute: ActivatedRoute, private restaurantService: RestaurantService){}

  ngOnInit(): void {
    this.getRestaurantsByLocation();
  }

  getRestaurantsByLocation(){
    let location = this.activatedRoute.snapshot.paramMap.get('location');

    console.log("this is location", location);

    if(location){
      this.location  = location
      this.restaurantService.getRestaurantByLocation(location).subscribe(res => {
        if(res.successful){
          console.log(res.result);
          this.restaurants = res.result;
        }
        else{
          console.log(res.errorMessage);
        }
      })
    }
  }

}
