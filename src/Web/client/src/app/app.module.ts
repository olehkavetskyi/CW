import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './account/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { MainComponent } from './main/main.component';
import { NgBusyModule } from 'ng-busy';
import { BusyInterceptor } from './core/interceptors/busy.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HeaderComponent } from './shared/components/header/header.component';
import { LayoutComponent } from './layout/layout.component';
import { ShopsComponent } from './shops/shops.component';
import { NoPageComponent } from './no-page/no-page.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { AddEmployeeOrderComponent } from './add-employee-order/add-employee-order.component';
import { AddCustomerOrderComponent } from './add-customer-order/add-customer-order.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    HeaderComponent,
    LayoutComponent,
    ShopsComponent,
    NoPageComponent,
    WarehouseComponent,
    AddEmployeeOrderComponent,
    AddCustomerOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    ReactiveFormsModule, 
    ToastrModule.forRoot(),
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: BusyInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
