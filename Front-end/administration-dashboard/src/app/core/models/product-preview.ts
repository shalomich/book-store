import { Base64Image } from '../interfaces/base64-image';

import { EntityPreview } from './entity-preview';

export class ProductPreview {

  public readonly id: number;

  public readonly name: string;

  public readonly titleImage?: Base64Image;

  public constructor(preview: ProductPreview) {
    this.id = preview.id;
    this.name = preview.name;
    this.titleImage = preview.titleImage;
  }
}
