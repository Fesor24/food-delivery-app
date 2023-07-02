import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from '../shared/models/apiResponse';
import { IRestaurant } from '../shared/models/restaurant';
import { environment } from '../environments/environment';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getRestaurants(){
    return this.http.get<IApiResponse<IRestaurant[], object, object>>(this.baseUrl + 'restaurants').pipe(
      map((response: IApiResponse<IRestaurant[], object, object>)  => response)
    )
  }
}
