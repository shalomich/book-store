import { BasketProduct } from '../models/basket-product';

export function getTotalCost(products: BasketProduct[]): number {
  return products.reduce((sum, a) => sum + (a.cost * a.quantity), 0);
}
