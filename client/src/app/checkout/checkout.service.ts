import { HttpClient, HttpHeaders } from '@angular/common/http';
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

    const token = localStorage.getItem('token');

    if(!token){
      return;
    }

    let headers = new HttpHeaders();

    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.post<IApiResponse<string, object, object>>(this.baseUrl + 'order', order, {headers})
    .subscribe((response) => {
      if(response.successful){
        localStorage.removeItem('cart_id');
        this.router.navigateByUrl('/');
        window.open(response.result);
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
