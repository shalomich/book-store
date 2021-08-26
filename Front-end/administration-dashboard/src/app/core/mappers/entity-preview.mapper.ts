import { Injectable } from '@angular/core';

import { EntityPreviewDto } from '../DTOs/entity-preview-dto';
import { EntityPreview } from '../models/entity-preview';

import { IMapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class FilmMapper implements IMapper<EntityPreviewDto, EntityPreview> {

  /** @inheritdoc */
  public toDto(data: EntityPreview): EntityPreviewDto {
    return {
      id: data.id,
      name: data.name,
      titleImage: data.titleImage,
    };
  }

  /** @inheritdoc */
  public fromDto(data: EntityPreviewDto): EntityPreview {
    return new EntityPreview({
      id: data.id,
      name: data.name,
      titleImage: data.titleImage,
    });
  }
}
