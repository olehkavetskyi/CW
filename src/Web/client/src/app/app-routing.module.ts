import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { authGuard } from './core/guards/auth.guard';
import { LayoutComponent } from './layout/layout.component';
import { ShopsComponent } from './shops/shops.component';
import { NoPageComponent } from './no-page/no-page.component';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { AddEmployeeOrderComponent } from './add-employee-order/add-employee-order.component';
import { AddCustomerOrderComponent } from './add-customer-order/add-customer-order.component';

const routes: Routes = [
  {
    path: '', 
    component: LayoutComponent, 
    canActivate: [authGuard], 
    children: [
      { path: '', component: MainComponent },
      { path: 'shops', component: ShopsComponent },
      { path: 'warehouse', component: WarehouseComponent },
      { path: 'add-employee-order', component: AddEmployeeOrderComponent },
      { path: 'add-customer-order', component: AddCustomerOrderComponent }
    ]
  },
  {
    path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)
  },
  { path: '**', component: NoPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
