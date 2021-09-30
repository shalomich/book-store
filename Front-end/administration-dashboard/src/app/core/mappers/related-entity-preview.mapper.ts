import {Injectable} from "@angular/core";
import {IMapper} from "./mapper/mapper";
import {ProductDto} from "../DTOs/product-dto";
import {ProductPreview} from "../models/product-preview";
import {RelatedEntityDto} from "../DTOs/related-entity-dto";
import {RelatedEntityPreview} from "../models/related-entity-preview";

@Injectable({ providedIn: 'root' })
export class RelatedEntityPreviewMapper implements IMapper<RelatedEntityDto, RelatedEntityPreview> {

  /** @inheritdoc */
  public toDto(data: RelatedEntityPreview): RelatedEntityDto {
    return {
      id: data.id,
      name: data.name
    };
  }

  /** @inheritdoc */
  public fromDto(data: RelatedEntityDto): RelatedEntityPreview {
    return new RelatedEntityPreview({
      id: data.id,
      name: data.name
    });
  }
}
