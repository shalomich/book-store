import { Injectable } from '@angular/core';

import { RelatedEntityPreviewDto } from '../DTOs/related-entity-preview-dto';
import { RelatedEntityPreview } from '../models/related-entity-preview';

import { IMapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class RelatedEntityPreviewMapper implements IMapper<RelatedEntityPreviewDto, RelatedEntityPreview> {

  /** @inheritdoc */
  public toDto(data: RelatedEntityPreview): RelatedEntityPreviewDto {
    return {
      id: data.id,
      name: data.name,
    };
  }

  /** @inheritdoc */
  public fromDto(data: RelatedEntityPreviewDto): RelatedEntityPreview {
    return new RelatedEntityPreview({
      id: data.id,
      name: data.name,
    });
  }
}
