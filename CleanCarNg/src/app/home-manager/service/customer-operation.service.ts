import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Operation } from '../models/operation';
import { CustomerOperationDB } from '../models/customer-operation-db';
import { Customer } from 'src/app/customer-manager/models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerOperationService {

  urlOperation = environment.urlApi + 'operations';
  urlCustomerOperation = environment.urlApi + 'orders';

  urlCustomer = environment.urlApi + 'customers';

  constructor(private http: HttpClient) { }

  getOperations(): Observable<Array<Operation>> {
    return this.http.get<Array<Operation>>(this.urlOperation);
  }

  postCustomerOperations(data: Array<CustomerOperationDB>): Observable<any> {
    return this.http.post<any>(this.urlCustomerOperation, data);
  }
}
