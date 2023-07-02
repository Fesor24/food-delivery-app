import { Component, OnInit } from '@angular/core';
import { IRestaurant } from '../shared/models/restaurant';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  showAddressModal = false;

  restaurants: IRestaurant[] = [];

  constructor(private homeService: HomeService) {}

  ngOnInit(): void {
    this.fetchRestaurants();
  }

  fetchRestaurants(){
    this.homeService.getRestaurants().subscribe((res) => {
     if(res.successful){
      console.log(res.result)
      this.restaurants = res.result;
     }
     else{
      console.log(res.errorMessage)
     }
    })

  }

  closeAddressModal() {
    this.showAddressModal = false;
  }

  openAddressModal() {
    this.showAddressModal = true;
  }

  receiveOutputValue(value: boolean) {
    this.showAddressModal = value;
  }
}
