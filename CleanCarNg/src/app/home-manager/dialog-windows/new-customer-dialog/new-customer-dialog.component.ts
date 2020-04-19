import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { CustomerService } from 'src/app/customer-manager/service/customer.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Customer } from 'src/app/customer-manager/models/customer';

@Component({
  selector: 'app-new-customer-dialog',
  templateUrl: './new-customer-dialog.component.html',
  styleUrls: ['./new-customer-dialog.component.css']
})
export class NewCustomerDialogComponent implements OnInit {
  
  id: number;
  firstName: string = '';
  secondName: string = '';
  phoneCustomer: string = '';

  @ViewChild('first', { static: false }) first?: any; 
  @ViewChild('second', { static: false }) second?: any; 
  @ViewChild('phone', { static: false }) phone?: any; 

  constructor(
    private customerService: CustomerService,
    public dialogRef: MatDialogRef<NewCustomerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {
  }

  onCreate(): void {
    let newCustomer = new Customer(0, this.firstName, this.secondName, this.phoneCustomer, 0, 0, null);
    this.customerService.postCustomer(newCustomer).subscribe(response => {
      if(response > 0) {
        newCustomer.Id = response;
        this.dialogRef.close(newCustomer);
      }
    }, 
    error => { 
      console.log(error);
    });
  }
  
  onClear(): void {
    this.first.control.reset();
    this.second.control.reset();
    this.phone.control.reset();
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}




