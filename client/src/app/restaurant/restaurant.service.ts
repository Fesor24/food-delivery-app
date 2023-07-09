import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IApiResponse } from '../shared/models/apiResponse';
import { IRestaurant } from '../shared/models/restaurant';
import { map } from 'rxjs';
import { IProducts } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getRestaurantByLocation(location: string){

    let params = new HttpParams();

    params = params.append('location', location);

    return this.http.get<IApiResponse<IRestaurant[], object, object>>(this.baseUrl + 'restaurants', {params: params})
    .pipe(
      map((response: IApiResponse<IRestaurant[], object, object>) => response)
    );
  }

  getFoodsByRestaurant(restaurantId: string){
    let params = new HttpParams();

    params = params.append('restaurantId', restaurantId);

    return this.http.get<IApiResponse<IProducts[], object, object>>(this.baseUrl + 'products', {params:params})
    .pipe(
      map(response => response)
    )
  }

}
