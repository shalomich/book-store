import { Injectable } from '@angular/core';

import { BasketProductDto } from '../DTOs/basket-product-dto';
import { BasketProduct } from '../models/basket-product';

import { Mapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class BasketProductMapper extends Mapper<BasketProductDto, BasketProduct> {

  /** @inheritdoc */
  public toDto(data: BasketProduct): BasketProductDto {
    return {
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      productQuantity: data.productQuantity,
      titleImage: data.titleImage,
      productId: data.productId,
    };
  }

  /** @inheritdoc */
  public fromDto(data: BasketProductDto): BasketProduct {
    return new BasketProduct({
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      productQuantity: data.productQuantity,
      titleImage: data.titleImage,
      productId: data.productId,
    });
  }
}
