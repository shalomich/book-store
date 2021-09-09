import { ImageDto } from '../DTOs/image-dto';

import { EntityPreview } from './entity-preview';

/** Entity preview class. */
export class ProductPreview extends EntityPreview {

  /** Entity item's title image. */
  public readonly titleImage?: ImageDto;

  public constructor(entity: ProductPreview) {
    super(entity);
    this.titleImage = entity.titleImage;
  }
}
