import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IRestaurant } from '../shared/models/restaurant';
import { HomeService } from './home.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  showAddressModal = false;


  location: string = '';

  restaurants: IRestaurant[] = [];

  constructor(private homeService: HomeService, private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.fetchRestaurants();
  }

  getAddressValue(){
    console.log("this is location", this.location);
    this.router.navigate(['/restaurant', this.location]);
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
    }, error => console.log(error));

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
