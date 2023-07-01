import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  showAddressModal = false;

  closeAddressModal() {
    this.showAddressModal = false;
  }

  openAddressModal() {
    this.showAddressModal = true;
  }

  receiveOutputValue(value:boolean){
    this.showAddressModal = value;
  }
}
