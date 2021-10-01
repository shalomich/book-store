import { Injectable } from '@angular/core';

import { RelatedEntityDto } from '../DTOs/related-entity-dto';
import { RelatedEntity } from '../models/related-entity';
import { Mapper } from './mapper/mapper';


/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class RelatedEntityMapper implements Mapper<RelatedEntityDto, RelatedEntity> {

  /** @inheritdoc */
  public toDto(data: RelatedEntity): RelatedEntityDto {
    return {
      id: data.id,
      name: data.name,
    };
  }

  /** @inheritdoc */
  public fromDto(data: RelatedEntityDto): RelatedEntity {
    return new RelatedEntity({
      id: data.id,
      name: data.name,
    });
  }
}
