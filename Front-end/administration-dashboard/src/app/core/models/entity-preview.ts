import { ImageDto } from '../DTOs/image-dto';

/** Entity preview class. */
export class EntityPreview {
  /** Entity item's id. */
  public readonly id: number;

  /** Entity item's name. */
  public readonly name: string;

  /** Entity item's title image. */
  public readonly titleImage: ImageDto;

  public constructor(entity: EntityPreview) {
    this.id = entity.id;
    this.name = entity.name;
    this.titleImage = entity.titleImage;
  }
}
