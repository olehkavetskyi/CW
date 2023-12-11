import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EmployeeOrderToSend } from '../shared/models/employeeOrderToSend';
import { AddCustomerOrderService } from './add-customer-order.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ShopProduct } from '../shared/models/shopProduct';
import { CustomerOrderToSend } from '../shared/models/customerOrderToSend';
import { CustomerOrderProduct } from '../shared/models/customerOrderProduct';

@Component({
  selector: 'app-add-customer-order',
  templateUrl: './add-customer-order.component.html',
  styleUrls: ['./add-customer-order.component.scss']
})
export class AddCustomerOrderComponent implements OnInit {
  findElemForm!: FormGroup;
  customerEmailForm!: FormGroup;

  customerOrderProducts: CustomerOrderProduct[] = [];
  customerOrder: CustomerOrderToSend = new CustomerOrderToSend();

  data: ShopProduct[] = []; 
  totalPages = 1;
  pageSize = 5; 
  currentPage = 1; 
  findElement = '';

  constructor(private addEmployeeOrderService: AddCustomerOrderService, 
    private fb: FormBuilder, 
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.loadItems(this.currentPage, this.pageSize, this.findElement);

    this.findElemForm = this.fb.group({
      findElement: ''
    });

    this.customerEmailForm = this.fb.group({
      email: new FormControl('', Validators.email)
    })
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
      error: () => {
        this.toastr.error('Помилка! Щось пішло не так.');
      }
    });
  }  

  addElem(event: any, product: ShopProduct) {
    const quantity = event.target.value;
    
    if (quantity < 0 || quantity > product.quantity) {
      return;
    }

    const existingOrderIndex = this.customerOrderProducts.findIndex(c => c.productId === product.productId);
    if (existingOrderIndex !== -1) {

      this.customerOrderProducts[existingOrderIndex].quantity = quantity;
    
      if (quantity == 0 && quantity == '') {
        this.customerOrderProducts.splice(existingOrderIndex, 1);
        return;
      }
    } else {
      if (quantity != 0 && quantity != '') {
        this.customerOrderProducts.push({ shopId: product.shopId, productId: product.productId, quantity: quantity });
      } 
    }
  }
  
  submitOrder() {
    this.customerOrder.products = this.customerOrderProducts;
    this.addEmployeeOrderService.sendOrders(this.customerOrder).subscribe({
      next: () => {
        this.toastr.success('Замовлення виконано!');
        this.router.navigateByUrl('/');
      },
      error: () => {
        this.toastr.error('Помилка! Щось пішло не так.');
      }
    });
  }

  onEmailChange() {
    if (this.customerEmailForm.valid) {
      console.log(this.customerEmailForm);
      this.customerOrder.customerEmail = this.customerEmailForm.controls['email'].value;
    }
  }
}
