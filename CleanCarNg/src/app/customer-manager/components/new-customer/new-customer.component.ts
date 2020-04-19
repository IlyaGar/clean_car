import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerService } from '../../service/customer.service';
import { Customer } from '../../models/customer';

@Component({
  selector: 'app-new-customer',
  templateUrl: './new-customer.component.html',
  styleUrls: ['./new-customer.component.css']
})
export class NewCustomerComponent implements OnInit {

  id: number;
  firstName: string = '';
  secondName: string = '';
  phoneCustomer: string = '';

  @ViewChild('first', { static: false }) first?: any; 
  @ViewChild('second', { static: false }) second?: any; 
  @ViewChild('phone', { static: false }) phone?: any; 

  constructor(
    private customerService: CustomerService
    ) { }

  ngOnInit(): void {

  }
  
  onCreate() {
    let newCustomer = new Customer(0, this.firstName, this.secondName, this.phoneCustomer, 0, 0, null);
    this.customerService.postCustomer(newCustomer).subscribe(response => {
      this.id = response;
    }, 
    error => { 
      console.log(error);
    });
  }

  onClear() {
    this.first.control.reset();
    this.second.control.reset();
    this.phone.control.reset();
  }
}
