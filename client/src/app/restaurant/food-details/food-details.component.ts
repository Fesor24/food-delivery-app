import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../restaurant.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-food-details',
  templateUrl: './food-details.component.html',
  styleUrls: ['./food-details.component.css']
})
export class FoodDetailsComponent implements OnInit{

  constructor(private restaurantService: RestaurantService, private activatedRoute: ActivatedRoute){}

  ngOnInit(): void {
    this.getProductsByRestaurantId();
  }

  getProductsByRestaurantId(){
    let restaurantId = this.activatedRoute.snapshot.paramMap.get('restaurantId');

    if(restaurantId){
      this.restaurantService.getFoodsByRestaurant(restaurantId).subscribe((res) => {
        if(res.successful){
          console.log('data', res.result)
        }
        else{
          console.log(res.errorMessage)
        }
      })
    }
  }

}
