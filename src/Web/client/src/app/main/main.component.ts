import { Component, OnInit } from '@angular/core';
import { MainService } from './main.service';
import { EmployeeDto } from '../shared/models/employeeDto';
import { Shop } from '../shared/models/shop';
import { Customer } from '../shared/models/customer';
import { EmployeeOrder } from '../shared/models/employeeOrders';
import { CustomerOrder } from '../shared/models/customerOrders';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  currentEmployee: EmployeeDto = null!;
  currentShop: Shop = null!;
  role!: string;
  userProductivity: number = null!;
  customers: Customer[] = [];
  averageProductivity: number = null!;
  employeeOrders: EmployeeOrder[] = [];
  customerOrders: CustomerOrder[] = [];

  constructor(private mainService: MainService) {}

  ngOnInit(): void {
    this.getCurrentEmployee();
    this.getCurrentShop();
    this.getAverageProductivity();
    this.getCustomersOrders();
    this.getEmployeesOrders();

    this.getCurrentRole();
  }

  getCurrentEmployee() {
    this.mainService.getCurrentEmployee().subscribe({
      next: (result: EmployeeDto) => this.currentEmployee = result,
      error: (err: any) => console.log(err)
    });
  }

  getCurrentShop() {
    this.mainService.getCurrentShop().subscribe({
      next: (result: Shop) => this.currentShop = result,
      error: (err: any) => console.log(err)
    });
  }

  getAverageProductivity() {
    this.mainService.getAverageProductivity().subscribe({
      next: (result: number) => this.averageProductivity = result,
      error: (err: any) => console.log(err)
    });
  }

  getCustomersOrders() {
    this.mainService.getCustomersOrders().subscribe({
      next: (result: CustomerOrder[]) => {
        console.log(result);
        this.customerOrders = result
      },
      error: (err: any) => console.log(err)
    });
  }

  getCurrentRole() {
    this.role = this.mainService.getUserRole()["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  }

  getEmployeesOrders() {
    this.mainService.getEmployeeOrders().subscribe({
      next: (result: EmployeeOrder[]) => {
        console.log(result);
        this.employeeOrders = result
      },
      error: (err: any) => console.log(err)
    });
  }
}
