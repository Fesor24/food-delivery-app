import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { IApiResponse } from '../shared/models/apiResponse';
import { IRestaurant } from '../shared/models/restaurant';

@Injectable({
  providedIn: 'root'
})
export class FoodService {

  baseUrl = "https://localhost:7050/api"

  constructor(private http: HttpClient) { }

  getRestaurants(){
    return this.http.get<IApiResponse<IRestaurant[], object, object>>(
      this.baseUrl + "/restaurants"
    )
  }
}
