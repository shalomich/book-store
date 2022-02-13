import {ProductPreviewDto} from "./product-preview-dto";

export interface ProductPreviewSetDto {
  previews: Array<ProductPreviewDto>,
  totalCount: number
}
