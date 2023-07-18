import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { IApiResponse } from '../shared/models/apiResponse';
import { IRestaurant } from '../shared/models/restaurant';
import { environment } from '../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { IShoppingCart, ShoppingCart } from '../shared/models/shoppingCart';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  baseUrl = environment.apiUrl;

  private shoppingCartSource : BehaviorSubject<IShoppingCart> = new BehaviorSubject<IShoppingCart>
  (new ShoppingCart())

  shoppingCart$ = this.shoppingCartSource.asObservable();

  constructor(private http: HttpClient) { }

  getRestaurants(){
    return this.http.get<IApiResponse<IRestaurant[], object, object>>(
      this.baseUrl + "/restaurants"
    )
  }
}
