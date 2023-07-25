import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IOrder } from '../shared/models/order';
import { IApiResponse } from '../shared/models/apiResponse';
import { Router } from '@angular/router';
import { IRestaurant } from '../shared/models/restaurant';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) { }

  createOrder(order: IOrder){
    return this.http.post<IApiResponse<object, object, object>>(this.baseUrl + 'order', order).subscribe((response) => {
      if(response.successful){
        this.router.navigateByUrl('/payment');
      }
    }, error => console.log(error));
  }

  fetchRestaurant(restaurantId: string){
    return this.http.get<IApiResponse<IRestaurant, object, object>>(this.baseUrl + 'restaurant?restaurantId=' + restaurantId).pipe(
      map((response: IApiResponse<IRestaurant, object, object>) => {
        if(response.successful){
          return response.result
        }else{
          console.log(response.errorMessage);
          return null;
        }
      })
    )
  }
}
