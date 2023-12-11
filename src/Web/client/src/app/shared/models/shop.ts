import { Employee } from "./employee";

export interface Shop {
  id: string;
  name: string;
  address: string;
  employees: Employee[];
}