import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Pagination } from '../shared/models/pagination';
import { ShopProduct } from '../shared/models/shopProduct';
import { CustomerOrderToSend } from '../shared/models/customerOrderToSend';

@Injectable({
  providedIn: 'root'
})
export class AddCustomerOrderService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWarehouseProducts(pageNumber: number, pageSize: number, findElement: string) {
    return this.http.get<Pagination<ShopProduct>>(`${this.apiUrl}products/shop`, { params: 
      {
        pageNumber: pageNumber, 
        pageSize: pageSize, 
        findElement: findElement
      }
    });
  }

  sendOrders(order: CustomerOrderToSend) {
    return this.http.post(`${this.apiUrl}customerOrders/add`, order);
  }
}