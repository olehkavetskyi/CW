import { Product } from "./product";

export interface Warehouse {
    id: string;
    warehouseProducts: WarehouseProduct[];
}

export interface WarehouseProduct {
    id: string;
    warehouseId: string;
    productId: string;
    product: Product;
    quantity: number;
    inputValue?: number; 
}