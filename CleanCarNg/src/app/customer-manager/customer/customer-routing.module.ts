import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerFormComponent } from '../customer-form/customer-form.component';
import { NewCustomerComponent } from '../components/new-customer/new-customer.component';
import { ListCustomersComponent } from '../components/list-customers/list-customers.component';


const routes: Routes = [
  { path: '', component: CustomerFormComponent, children: [
    { path: 'new', component: NewCustomerComponent },
    { path: 'list', component: ListCustomersComponent },
  ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
