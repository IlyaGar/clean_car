import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { environment } from 'src/environments/environment';
import { CustomerOperation } from '../models/customer-operation';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  urlCustomer = environment.urlApi + 'customers';
  urlCustomerOperaation = environment.urlApi + 'orders';

  constructor(private http: HttpClient) { }

  getCustomers(): Observable<Array<Customer>> {
    return this.http.get<Array<Customer>>(this.urlCustomer);
  }

  getCustomerOperations(id: number): Observable<Array<CustomerOperation>> {
    return this.http.get<Array<CustomerOperation>>(`${this.urlCustomerOperaation}/${id}`);
  }

  getPdfReport(id: number): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'multipart/form-data');
    return this.http.get<any>(`${this.urlCustomer}/${id}`, { headers, responseType: 'blob' as 'json' });
  }

  postCustomer(customer: Customer): Observable<number> {
    return this.http.post<number>(this.urlCustomer, customer);
  }
}
