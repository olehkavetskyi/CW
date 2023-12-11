import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEmployeeOrderComponent } from './add-employee-order.component';

describe('AddEmployeeOrderComponent', () => {
  let component: AddEmployeeOrderComponent;
  let fixture: ComponentFixture<AddEmployeeOrderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEmployeeOrderComponent]
    });
    fixture = TestBed.createComponent(AddEmployeeOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
