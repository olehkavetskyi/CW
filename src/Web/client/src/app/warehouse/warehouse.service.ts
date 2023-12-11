import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Warehouse } from '../shared/models/warehouse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWarehouse() {
    return this.http.get<Warehouse>(`${this.apiUrl}warehouse/all`);
  }
}
