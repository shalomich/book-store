import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityPreviewMapper } from '../mappers/related-entity-preview.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class RelatedEntityService extends EntityService {

  public constructor(
    http: HttpClient,
    private readonly relatedEntityPreviewMapper: RelatedEntityPreviewMapper,
  ) {
    super(http);
  }

  public getRelatedEntityPage(relatedEntityType: string): Observable<RelatedEntity[]> {
    const entityItems$ = super.getEntityPage<RelatedEntityDto>(relatedEntityType);

    return entityItems$.pipe(
      map(data => data.map(item => this.relatedEntityPreviewMapper.fromDto(item))),
    );
  }
}
