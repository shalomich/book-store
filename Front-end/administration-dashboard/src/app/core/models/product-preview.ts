import { Image } from '../interfaces/image';

import { EntityPreview } from './entity-preview';

export class ProductPreview {

  public readonly id: number;

  public readonly name: string;

  public readonly titleImage?: Image;

  public constructor(preview: ProductPreview) {
    this.id = preview.id;
    this.name = preview.name;
    this.titleImage = preview.titleImage;
  }
}
