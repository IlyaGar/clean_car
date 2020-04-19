import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeFormComponent } from './home-manager/home-form/home-form.component';
import { NavbarComponent } from './navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './common/material-module';
import { HttpClientModule } from '@angular/common/http';
import { NewCustomerDialogComponent } from './home-manager/dialog-windows/new-customer-dialog/new-customer-dialog.component';
import { SelectCustomerDialogComponent } from './home-manager/dialog-windows/select-customer-dialog/select-customer-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeFormComponent,
    NavbarComponent,
    NewCustomerDialogComponent,
    SelectCustomerDialogComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    MaterialModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
