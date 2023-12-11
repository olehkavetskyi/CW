import { Component, OnInit } from '@angular/core';
import { WarehouseService } from './warehouse.service';
import { Warehouse, WarehouseProduct } from '../shared/models/warehouse';
import { AddEmployeeOrderService } from '../add-employee-order/add-employee-order.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Product } from '../shared/models/product';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss']
})
export class WarehouseComponent implements OnInit {
  findElemForm!: FormGroup;
  data: WarehouseProduct[] = []; 
  totalPages = 1;
  pageSize = 5; 
  currentPage = 1; 
  place = 'warehouse';
  findElement = '';

  constructor(private addEmployeeOrderService: AddEmployeeOrderService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadItems(this.currentPage, this.pageSize, this.place, this.findElement);

    this.findElemForm = this.fb.group({
      findElement: ''
    });
  }

  onPageChange(pageIndex: number) {
    if (pageIndex != 0 || pageIndex > this.totalPages) {
      this.currentPage = pageIndex;
      this.loadItems(this.currentPage, this.pageSize, this.place, this.findElement);
    }
  }

  find() {
    this.findElement = this.findElemForm.controls['findElement'].value;
    this.loadItems(this.currentPage, this.pageSize, this.place, this.findElement);
  }

  loadItems(pageNumber: number, pageSize: number, place: string, findElement: string) {
    this.addEmployeeOrderService.getWarehouseProducts(pageNumber, pageSize, findElement).subscribe({
      next: result => {
        this.data = result.data;
        this.totalPages = result.count / pageSize;
      },
      error: err => console.log(err)
    })
  }
}
