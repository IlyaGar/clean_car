import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NewCustomerDialogComponent } from '../dialog-windows/new-customer-dialog/new-customer-dialog.component';
import { Customer } from 'src/app/customer-manager/models/customer';
import { SelectCustomerDialogComponent } from '../dialog-windows/select-customer-dialog/select-customer-dialog.component';
import { CustomerOperationService } from '../service/customer-operation.service';
import { Operation } from '../models/operation';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { CustomerOperation } from 'src/app/customer-manager/models/customer-operation';
import { CustomerOperationDB } from '../models/customer-operation-db';

export interface Transaction {
  item: string;
  cost: number;
}

@Component({
  selector: 'app-home-form',
  templateUrl: './home-form.component.html',
  styleUrls: ['./home-form.component.css']
})
export class HomeFormComponent implements OnInit {

  displayedColumnsO: string[] = ['select', 'item', 'cost'];

  displayedColumns: string[] = ['select', 'name', 'price'];
  dataSource: any;
  selection = new SelectionModel<Operation>(true, []);

  selectedCustomer: Customer = null;
  listOperations: Array<Operation> = [];

  constructor(
    public dialog: MatDialog,
    private customerOperationService: CustomerOperationService
  ) { }

  ngOnInit(): void {
  }

  onOpenCreateCustomerForm() {
    const dialogRef = this.dialog.open(NewCustomerDialogComponent, {
      width: '25em',
    });
    dialogRef.afterClosed().subscribe((result: any) => {
      if(result) {
        this.selectedCustomer = result;
        this.loadListOperations();
      }
    });
  }

  onOpenSelectCustomerForm() {
    const dialogRef = this.dialog.open(SelectCustomerDialogComponent, {
      // width: '25em',
    });
    dialogRef.afterClosed().subscribe((result: any) => {
      if(result) {
        this.selectedCustomer = result;
        this.loadListOperations();
      }
    });
  }

  loadListOperations() {
    this.customerOperationService.getOperations().subscribe(response => {
      this.listOperations = response;
      this.dataSource = new MatTableDataSource<Operation>(this.listOperations);
    }, 
    error => { 
      console.log(error);
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }
  
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }
  
  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Operation): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.Name + 1}`;

  }
  
  getTotalCost() {
    return this.selection.selected.map(t => t.Price).reduce((acc, value) => acc + value, 0);
  }

  onClearCustomer() {
    this.selectedCustomer = null;
  }

  onCreatOrder() {
    let listCustomerOperations = this.selection.selected.map(obj => ( new CustomerOperationDB(0, new Date, obj.Price, obj.Id, this.selectedCustomer.Id)));
    this.customerOperationService.postCustomerOperations(listCustomerOperations).subscribe(response => {
      if(response) 
      this.selection.clear();
    }, 
    error => { 
      console.log(error);
    });
  }
}
