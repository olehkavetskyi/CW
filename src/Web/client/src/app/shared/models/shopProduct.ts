import { Product } from "./product";

export interface ShopProduct {
    id: string;
    shopId: string;
    productId: string;
    product: Product;
    quantity: number;
    inputValue?: number; 
}