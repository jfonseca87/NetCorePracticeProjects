import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SomeService {

  constructor(private httpClient: HttpClient) { }

  getSomeData(): Observable<string> {
    return this.httpClient.get<string>(`${environment.baseUrl}some`);
  }
}
