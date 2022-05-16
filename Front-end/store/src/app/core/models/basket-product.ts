import { Image } from '../interfaces/image';

export class BasketProduct {
  public id: number;

  public name: string;

  public cost: number;

  public quantity: number;

  public titleImage: Image;

  public productId: number;

  constructor(product: BasketProduct) {
    this.id = product.id;
    this.name = product.name;
    this.cost = product.cost;
    this.quantity = product.quantity;
    this.titleImage = product.titleImage;
    this.productId = product.productId;
  }
}
