import {ProductPreview} from "./product-preview";

export class ProductPreviewSet {
  public previews: ProductPreview[];

  public totalCount: number;

  constructor(previews: ProductPreview[], totalCount: number) {
    this.previews = previews;
    this.totalCount = totalCount;
  }
}
