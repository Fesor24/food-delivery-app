import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-restaurant-nav',
  templateUrl: './restaurant-nav.component.html',
  styleUrls: ['./restaurant-nav.component.css']
})
export class RestaurantNavComponent implements OnInit {

  user$! : Observable<IUser | null>

  constructor(private accountService: AccountService){}


  ngOnInit(): void {
    this.user$ = this.accountService.user$;
  }

  logout(){
    this.accountService.logout();
  }

}
