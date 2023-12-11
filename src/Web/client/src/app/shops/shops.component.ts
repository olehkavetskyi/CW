import { Component, OnInit } from '@angular/core';
import { ShopsService } from './shops.service';
import { ShopDto } from '../shared/models/shopDto';

@Component({
  selector: 'app-shops',
  templateUrl: './shops.component.html',
  styleUrls: ['./shops.component.scss']
})
export class ShopsComponent implements OnInit {
  shops!: ShopDto[];
  constructor(private shopsService: ShopsService) {}

  ngOnInit(): void {
    this.getShops();
  }

  getShops() {
    this.shopsService.getShopList().subscribe({
      next: (shops) => this.shops = shops,
      error: (err) => console.log(err)
    })
  }
}
