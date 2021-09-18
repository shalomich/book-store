import { Image } from '../interfaces/image';

import { EntityPreviewDto } from './entity-preview-dto';


export interface ProductPreviewDto extends EntityPreviewDto {

  /** Entity item's title image. */
  readonly titleImage?: Image;
}
