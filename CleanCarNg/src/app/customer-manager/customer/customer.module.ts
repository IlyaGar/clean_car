import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerFormComponent } from '../customer-form/customer-form.component';
import { MaterialModule } from 'src/app/common/material-module';
import { NewCustomerComponent } from '../components/new-customer/new-customer.component';
import { ListCustomersComponent } from '../components/list-customers/list-customers.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    CustomerFormComponent,
    NewCustomerComponent,
    ListCustomersComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    MaterialModule,
    CustomerRoutingModule,
    HttpClientModule
  ],
  providers: [
    DatePipe
  ]
})
export class CustomerModule { }
