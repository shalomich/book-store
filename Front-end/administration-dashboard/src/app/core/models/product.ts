import { Album } from '../interfaces/album';

import { Entity } from './entity';

export class Product extends Entity {
  cost: number;

  quantity: number;

  description: string;

  album: Album;

  public constructor(product: Product) {
    super(product);

    this.cost = product.cost;
    this.quantity = product.quantity;
    this.description = product.description;
    this.album = product.album;
  }
}
