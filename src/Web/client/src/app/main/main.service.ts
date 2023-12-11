import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeeDto } from '../shared/models/employeeDto';
import { Shop } from '../shared/models/shop';
import { EmployeeOrder } from '../shared/models/employeeOrders';
import { CustomerOrder } from '../shared/models/customerOrders';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class MainService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCurrentEmployee() {
    return this.http.get<EmployeeDto>(`${this.apiUrl}employees/current`);
  }

  getCurrentShop() {
    return this.http.get<Shop>(`${this.apiUrl}shops/current`);
  }

  getAverageProductivity() {
    return this.http.get<number>(`${this.apiUrl}shops/average-productivity`);
  }

  getEmployeeOrders() {
    return this.http.get<EmployeeOrder[]>(`${this.apiUrl}employeeorders/current`);
  }

  getCustomersOrders() {
    return this.http.get<CustomerOrder[]>(`${this.apiUrl}customerorders/current`);
  }

  getUserRole() {
    const jwtToken = localStorage.getItem('token');

    if (jwtToken != null) {
      return jwtDecode(jwtToken) as any;

    }
    
    return null;
  }
}
