import { Image } from '../interfaces/image';

export class ProductPreview {
  public id: number;

  public name: string;

  public titleImage: Image;

  public cost: number;

  public isInBasket: boolean;


  public constructor(productPreview: ProductPreview) {
    this.id = productPreview.id;
    this.name = productPreview.name;
    this.titleImage = productPreview.titleImage;
    this.cost = productPreview.cost;
    this.isInBasket = productPreview.isInBasket;
  }
}
