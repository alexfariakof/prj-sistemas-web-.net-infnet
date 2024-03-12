import { Customer } from 'src/app/model';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private routeUrl:string = 'customer';

  constructor(public httpClient: HttpClient) { }

  public Create(customer: Customer): Observable<any> {
    return this.httpClient.post<Customer>(`${ this.routeUrl }`, customer);
  }

}
