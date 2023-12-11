import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';
import { environment } from 'src/environments/environment';
import { EmployeeOrder } from '../shared/models/employeeOrders';
import { EmployeeOrderToSend } from '../shared/models/employeeOrderToSend';
import { WarehouseProduct } from '../shared/models/warehouse';

@Injectable({
  providedIn: 'root'
})
export class AddEmployeeOrderService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWarehouseProducts(pageNumber: number, pageSize: number, findElement: string) {
    return this.http.get<Pagination<WarehouseProduct>>(`${this.apiUrl}products/warehouse`, { params: 
      {
        pageNumber: pageNumber, 
        pageSize: pageSize, 
        findElement: findElement
      }
    }
    );
  }

  sendOrders(orders: EmployeeOrderToSend[]) {
    return this.http.post(`${this.apiUrl}employeeOrders/add`, orders);
  }
}
