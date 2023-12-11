import { TestBed } from '@angular/core/testing';

import { AddCustomerOrderService } from './add-customer-order.service';

describe('AddCustomerOrderService', () => {
  let service: AddCustomerOrderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddCustomerOrderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
