import { Base64Image } from '../interfaces/base64-image';

export class BasketProduct {
  public id: number;

  public name: string;

  public cost: number;

  public quantity: number;

  public titleImage: Base64Image;

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
