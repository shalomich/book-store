import { Injectable } from '@angular/core';

import { ProductPreview } from '../models/product-preview';

import { ProductDto } from '../DTOs/product-dto';
import { Base64Image } from '../interfaces/base64-image';

import { Mapper } from './mapper/mapper';
import { IFromDtoMapper } from './mapper/from-dto-mapper';

@Injectable({ providedIn: 'root' })
export class ProductPreviewMapper implements IFromDtoMapper<ProductDto, ProductPreview> {

  /** @inheritdoc */
  public fromDto(data: ProductDto): ProductPreview {
    return new ProductPreview({
      id: data.id,
      name: data.name,
      titleImage: data.album?.images.find(image => image.name === data.album.titleImageName),
    });
  }
}
