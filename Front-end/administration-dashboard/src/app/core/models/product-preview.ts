import { Image } from '../interfaces/image';

import { EntityPreview } from './entity-preview';

/** Entity preview class. */
export class ProductPreview extends EntityPreview {

  /** Entity item's title image. */
  public readonly titleImage?: Image;

  public constructor(entity: ProductPreview) {
    super(entity);
    this.titleImage = entity.titleImage;
  }
}
