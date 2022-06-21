import { Image } from '../interfaces/image';

export class BasketProduct {
  public id: number;

  public name: string;

  public cost: number;

  public quantity: number;

  public productQuantity: number;

  public titleImage: Image;

  public productId: number;

  constructor(product: BasketProduct) {
    this.id = product.id;
    this.name = product.name;
    this.cost = product.cost;
    this.quantity = product.quantity;
    this.productQuantity = product.productQuantity;
    this.titleImage = product.titleImage;
    this.productId = product.productId;
  }
}
