import { Component, OnInit } from '@angular/core';
import { AddEmployeeOrderService } from './add-employee-order.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EmployeeOrderToSend } from '../shared/models/employeeOrderToSend';
import { WarehouseProduct } from '../shared/models/warehouse';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-employee-order',
  templateUrl: './add-employee-order.component.html',
  styleUrls: ['./add-employee-order.component.scss']
})
export class AddEmployeeOrderComponent implements OnInit {
  findElemForm!: FormGroup;
  employeeOrderToSend!: EmployeeOrderToSend
  employeeOrders: EmployeeOrderToSend[] = [];

  data: WarehouseProduct[] = []; 
  totalPages = 1;
  pageSize = 5; 
  currentPage = 1; 
  findElement = '';

  constructor(private addEmployeeOrderService: AddEmployeeOrderService, 
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadItems(this.currentPage, this.pageSize, this.findElement);

    this.findElemForm = this.fb.group({
      findElement: ''
    });
  }

  onPageChange(pageIndex: number) {
    if (pageIndex != 0 || pageIndex > this.totalPages) {
      this.currentPage = pageIndex;
      this.loadItems(this.currentPage, this.pageSize, this.findElement);
    }
  }

  find() {
    this.findElement = this.findElemForm.controls['findElement'].value;
    this.loadItems(this.currentPage, this.pageSize, this.findElement);
  }

  loadItems(pageNumber: number, pageSize: number, findElement: string) {
    this.addEmployeeOrderService.getWarehouseProducts(pageNumber, pageSize, findElement).subscribe({
      next: result => {
        this.data = result.data.map(item => ({ ...item, inputValue: item.inputValue || 0 }));
        this.totalPages = result.count / pageSize;
      },
      error: err => {
        this.toastr.error('Помилка! Щось пішло не так.');
      }
    });
  }  

  addElem(event: any, product: WarehouseProduct) {
    const quantity = event.target.value;
  
    if (quantity < 0 || quantity > product.quantity) {
      return;
    }

    const existingOrderIndex = this.employeeOrders.findIndex(p => p.productId === product.id);

    if (existingOrderIndex !== -1) {
      this.employeeOrders[existingOrderIndex].quantity = quantity;
  
      if (quantity === 0 || quantity == '') {
        this.employeeOrders.splice(existingOrderIndex, 1);
      }
    } else {
      if (quantity !== 0) {
        this.employeeOrders.push({ warehouseId: product.warehouseId, productId: product.product.id, quantity: quantity });
      }
    }
  }
  
  submitOrder() {
    this.addEmployeeOrderService.sendOrders(this.employeeOrders).subscribe({
      next: value => {
        this.toastr.success('Замовлення виконано!');
        this.router.navigateByUrl('/');
      },
      error: err => {
        this.toastr.error('Помилка! Щось пішло не так.');
      }
    });
  }
}
