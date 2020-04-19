import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectCustomerDialogComponent } from './select-customer-dialog.component';

describe('SelectCustomerDialogComponent', () => {
  let component: SelectCustomerDialogComponent;
  let fixture: ComponentFixture<SelectCustomerDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectCustomerDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectCustomerDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
