import { Component, EventEmitter, HostListener, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  hasScrolledPastPoint = false;

  @Output() showAddressModal = false;

  @Output() eventEmit = new EventEmitter<boolean>();

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const desiredPoint = 450;
    const scrollPosition =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;

    this.hasScrolledPastPoint = scrollPosition > desiredPoint;
  }

  openAddressModal() {
    this.eventEmit.emit(true);
  }
}
