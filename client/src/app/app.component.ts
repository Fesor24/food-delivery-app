import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  hasScrolledPastPoint = false;

  showAddressModal = false;

  @HostListener('window:scroll', [])

  onWindowScroll(){
    const desiredPoint = 450;
    const scrollPosition = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;

    this.hasScrolledPastPoint = scrollPosition > desiredPoint
  }

  closeAddressModal(){
    this.showAddressModal = false;
  }

  openAddressModal(){
    this.showAddressModal = true;
  }
}
