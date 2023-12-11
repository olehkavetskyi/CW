import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ShopDto } from '../shared/models/shopDto';

@Injectable({
  providedIn: 'root'
})
export class ShopsService {
  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getShopList() {
    return this.http.get<ShopDto[]>(`${this.apiUrl}shops/all`);
  }
}
