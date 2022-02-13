import {Injectable} from "@angular/core";
import {Mapper} from "./mapper/mapper";
import {ProductPreviewDto} from "../DTOs/product-preview-dto";
import {ProductPreview} from "../models/product-preview";
import {IFromDtoMapper} from "./mapper/from-dto-mapper";
import {ProductPreviewSetDto} from "../DTOs/product-preview-set-dto";
import {ProductPreviewSet} from "../models/product-preview-set";
import {ProductPreviewMapper} from "./product-preview.mapper";

@Injectable({ providedIn: 'root' })
export class ProductPreviewSetMapper implements IFromDtoMapper<ProductPreviewSetDto, ProductPreviewSet> {

  private productPreviewMapper: ProductPreviewMapper;

  constructor(productPreviewMapper: ProductPreviewMapper) {
    this.productPreviewMapper = productPreviewMapper;
  }

  public fromDto(setDto: ProductPreviewSetDto): ProductPreviewSet {
    return new ProductPreviewSet( setDto.previews.map(previewDto => this.productPreviewMapper.fromDto(previewDto)), setDto.totalCount);
  }
}
