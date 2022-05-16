import { BasketProduct } from '../models/basket-product';

export interface OrderDto {
  id: number;

  state: string;

  userName: string;

  email: string;

  phoneNumber: string;

  address: string;

  orderReceiptMethod: string;

  paymentMethod: string;

  placedDate: string;

  deliveredDate: string;

  products: BasketProduct[];
}
