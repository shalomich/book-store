import { ImageDto } from './image-dto';

/** Entity preview DTO object. */
export interface EntityPreviewDto {

  /** Entity item's id. */
  readonly id: number;

  /** Entity item's name. */
  readonly name: string;

  /** Entity item's title image. */
  readonly titleImage: ImageDto;
}
