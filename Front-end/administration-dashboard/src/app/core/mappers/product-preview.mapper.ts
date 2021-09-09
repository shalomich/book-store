import { Injectable } from '@angular/core';

import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreview } from '../models/product-preview';

import { IMapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class ProductPreviewMapper implements IMapper<ProductPreviewDto, ProductPreview> {

  /** @inheritdoc */
  public toDto(data: ProductPreview): ProductPreviewDto {
    return {
      id: data.id,
      name: data.name,
      titleImage: data.titleImage,
    };
  }

  /** @inheritdoc */
  public fromDto(data: ProductPreviewDto): ProductPreview {
    return new ProductPreview({
      id: data.id,
      name: data.name,
      titleImage: data.titleImage,
    });
  }
}
