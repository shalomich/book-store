import { Image } from '../interfaces/image';

export interface ProductPreviewDto {
  readonly id: number;

  readonly name: string;

  readonly titleImage: Image;

  readonly cost: number;

  readonly isInBasket: boolean;
}
