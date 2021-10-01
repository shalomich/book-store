import {Injectable} from "@angular/core";

import {RelatedEntityDto} from "../DTOs/related-entity-dto";
import {RelatedEntityPreview} from "../models/related-entity-preview";
import { IFromDtoMapper } from './mapper/from-dto-mapper';

@Injectable({ providedIn: 'root' })
export class RelatedEntityPreviewMapper implements IFromDtoMapper<RelatedEntityDto, RelatedEntityPreview> {

  /** @inheritdoc */
  public fromDto(data: RelatedEntityDto): RelatedEntityPreview {
    return new RelatedEntityPreview({
      id: data.id,
      name: data.name
    });
  }
}
