import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';

import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreview } from '../models/product-preview';

import { Mapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class ProductPreviewMapper extends Mapper<ProductPreviewDto, ProductPreview> {

  /** @inheritdoc */
  public toDto(data: ProductPreview): ProductPreviewDto {
    return {
      id: data.id,
      name: data.name,
      cost: data.cost,
      titleImage: data.titleImage,
    };
  }

  /** @inheritdoc */
  public fromDto(data: ProductPreviewDto): ProductPreview {
    return new ProductPreview({
      id: data.id,
      name: data.name,
      cost: data.cost,
      titleImage: data.titleImage,
    });
  }
}
