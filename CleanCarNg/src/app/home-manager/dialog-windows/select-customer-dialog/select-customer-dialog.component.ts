import { Component, OnInit, Inject } from '@angular/core';
import { CustomerService } from 'src/app/customer-manager/service/customer.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NewCustomerDialogComponent } from '../new-customer-dialog/new-customer-dialog.component';
import { Customer } from 'src/app/customer-manager/models/customer';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-select-customer-dialog',
  templateUrl: './select-customer-dialog.component.html',
  styleUrls: ['./select-customer-dialog.component.css']
})
export class SelectCustomerDialogComponent implements OnInit {

  selectedRowIndex: any;
  listCustomers: Array<Customer> = [];
  selectedCustomer: Customer;
  displayedColumns: string[] = ['fname', 'sname', 'phone'];
  dataSource: any;

  constructor(
    private customerService: CustomerService,
    public dialogRef: MatDialogRef<NewCustomerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {
    this.loadListCustomers();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  loadListCustomers() {
    this.customerService.getCustomers().subscribe(response => {
      this.dataSource = new MatTableDataSource(response);
    }, 
    error => { 
      console.log(error);
    });
  }

  onOkClick(customer: Customer): void {
    this.dialogRef.close(customer);
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
