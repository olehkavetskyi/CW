import { CustomerOrderProduct } from "./customerOrderProduct";

export class CustomerOrderToSend {
    customerEmail: string | null = null;
    products: CustomerOrderProduct[] = [];
}
  