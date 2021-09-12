import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntityPreview } from '../models/related-entity-preview';
import { RelatedEntityPreviewMapper } from '../mappers/related-entity-preview.mapper';
import { RelatedEntityPreviewDto } from '../DTOs/related-entity-preview-dto';

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

  public getRelatedEntityPage(relatedEntityType: string): Observable<RelatedEntityPreview[]> {
    const entityItems$ = super.getEntityPage<RelatedEntityPreviewDto>(relatedEntityType);

    return entityItems$.pipe(
      map(data => data.map(item => this.relatedEntityPreviewMapper.fromDto(item))),
    );
  }
}
