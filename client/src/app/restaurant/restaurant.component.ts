import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from './restaurant.service';
import { IRestaurant } from '../shared/models/restaurant';
import { AccountService } from '../account/account.service';
import { IUser } from '../shared/models/user';
import { Observable } from 'rxjs';

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


    if(location){
      this.location  = location
      localStorage.setItem('location', location);
      this.restaurantService.getRestaurantByLocation(location).subscribe(res => {
        if(res.successful){
          console.log(res.result);
          this.restaurants = res.result;
        }
        else{
          console.log(res.errorMessage);
        }
      }, error => console.log(error));
    }
  }

}
