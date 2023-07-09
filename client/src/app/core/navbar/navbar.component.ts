import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {

  constructor(){}

  ngOnInit(): void {
    this.screenWidth = window.innerWidth;
    window.addEventListener('resize', this.onWindowResize.bind(this))
  }
  hasScrolledPastPoint = false;

  screenWidth?: number;

  @Output() showAddressModal = false;

  @Output() eventEmit = new EventEmitter<boolean>();

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const desiredPoint =  this.screenWidth! < 764 ? 685 : 250;
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

  onWindowResize(event: Event) {
    this.screenWidth = window.innerWidth;
  }
}
