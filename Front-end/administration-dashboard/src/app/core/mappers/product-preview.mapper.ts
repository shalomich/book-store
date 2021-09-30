import { Injectable } from '@angular/core';

import { ProductPreview } from '../models/product-preview';

import { IMapper } from './mapper/mapper';
import {ProductDto} from "../DTOs/product-dto";
import {Image} from "../interfaces/image";

@Injectable({ providedIn: 'root' })
export class ProductPreviewMapper implements IMapper<ProductDto, ProductPreview> {

  public toDto(data: ProductPreview): ProductDto {
    return {
      id: data.id,
      name: data.name,
      cost: 0,
      quantity: 0,
      description: "",
      album: {
        titleImageName: "",
        images: []
      }
    }
  }

  /** @inheritdoc */
  public fromDto(data: ProductDto): ProductPreview {
    return new ProductPreview({
      id: data.id,
      name: data.name,
      titleImage: data.album.images.find(image => image.name == data.album.titleImageName),
    });
  }
}
