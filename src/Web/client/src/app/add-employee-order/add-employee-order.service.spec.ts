import { TestBed } from '@angular/core/testing';

import { AddEmployeeOrderService } from './add-employee-order.service';

describe('AddEmployeeOrderService', () => {
  let service: AddEmployeeOrderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddEmployeeOrderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
