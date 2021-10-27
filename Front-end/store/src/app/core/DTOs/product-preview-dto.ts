import { Base64Image } from '../interfaces/base64-image';

export interface ProductPreviewDto {
  readonly id: number;

  readonly name: string;

  readonly titleImage: Base64Image;

  readonly cost: number;
}
