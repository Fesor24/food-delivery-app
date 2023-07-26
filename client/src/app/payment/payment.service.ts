import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IApiResponse } from '../shared/models/apiResponse';
import { map } from 'rxjs';
import { ISummaryOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  verifyPayment(trxref: string){
    return this.http.get<IApiResponse<ISummaryOrder, object, object>>(this.baseUrl + 'verify-payment?trxref=' + trxref).pipe(
      map((response : IApiResponse<ISummaryOrder, object, object>) => {
        if(response.successful){
          return response.result
        }else{
          console.log(response.errorMessage);
          return;
        }
      })
    )
  }
}
