import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  logIn(user: User): Observable<string> {
    return this.httpClient.post<string>(`${environment.baseUrl}account`, user, {withCredentials: true});
  }
}
