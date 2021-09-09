import { EntityPreviewDto } from './entity-preview-dto';
import { ImageDto } from './image-dto';

export interface ProductPreviewDto extends EntityPreviewDto {

  /** Entity item's title image. */
  readonly titleImage?: ImageDto;
}
