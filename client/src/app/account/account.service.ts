import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IUser } from '../shared/models/user';
import { IApiResponse } from '../shared/models/apiResponse';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl

  private userSource: BehaviorSubject<IUser | null> = new BehaviorSubject<IUser | null>(null);

  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient) { }

  login(data: any){
    return this.http.post<IApiResponse<IUser, object, object>>(this.baseUrl + "login", data).pipe(
      map((response: IApiResponse<IUser, object, object>) => {
        if(response.successful){
          localStorage.setItem('token', response.result.token);
          this.userSource.next(response.result);
        }
      })
    )
  }

   register(data: any){
    return this.http.post<IApiResponse<IUser, object, object>>(this.baseUrl + "register", data).pipe(
      map((response: IApiResponse<IUser, object, object>) => {
        if(response.successful){
          localStorage.setItem('token', response.result.token);
          this.userSource.next(response.result);
        }
      })
    )
  }

  loadCurrentUser(token: string){
    let headers = new HttpHeaders;

    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IApiResponse<IUser, object, object>>(this.baseUrl + 'user', {headers}).pipe(
      map((response: IApiResponse<IUser, object, object>) => {
        localStorage.setItem('token', response.result.token);
        this.userSource.next(response.result);
      })
    )
  }

  checkEmailExist(email: string){
    return this.http.get<IApiResponse<boolean, object, object>>(this.baseUrl + "email-exists?email=" + email).pipe(
      map((response : IApiResponse<boolean, object, object>) => response)
    )
  }
}
