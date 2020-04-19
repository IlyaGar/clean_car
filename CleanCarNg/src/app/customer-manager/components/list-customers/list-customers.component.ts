import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../service/customer.service';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-list-customers',
  templateUrl: './list-customers.component.html',
  styleUrls: ['./list-customers.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ListCustomersComponent implements OnInit {

  listCustomers: Array<Customer> = [];
  displayedColumns: string[] = ['name', 'date', 'price', 'action'];
  columnsToDisplay: string[] = ['name', 'count', 'sum'];
  expandedElement: Customer | null;
  url: any;

  constructor(
    private datePipe: DatePipe,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.loadListCustomers();
  }

  loadListCustomers() {
    this.customerService.getCustomers().subscribe(response => {
      this.listCustomers = response;
    }, 
    error => { 
      console.log(error);
    });
  }

  onClickCustomer(element: Customer) {
    this.loadCustomerOperations(element);
  }

  loadCustomerOperations(element: Customer) {
    this.customerService.getCustomerOperations(element.Id).subscribe(response => {
      element.CustomerOperations = response;
    }, 
    error => { 
      console.log(error);
    });
  }

  onClickPdfReport(element: Customer) {
    let date = new Date;
    let dateString = this.datePipe.transform(date, 'yyyy-MM-dd');
    this.customerService.getPdfReport(element.Id).subscribe(response => {
      const data = new Blob([response], { type: 'application/pdf' });
      const fileURL = URL.createObjectURL(data);
      window.open(fileURL, '_blank');

      var link = document.createElement('a');
      link.href = URL.createObjectURL(data);
      link.download = `${element.FirstName}_${element.SecondName}_${dateString}.pdf`;
      link.click();
    }, 
    error => { 
      console.log(error);
    });
  }
}