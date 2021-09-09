import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { EntityPreview } from '../models/entity-preview';
import { EntityPreviewDto } from '../DTOs/entity-preview-dto';
import { EntityPreviewMapper } from '../mappers/entity-preview.mapper';
import { API_FORM_ENTITY_URI } from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class EntityPreviewService {

  constructor(
    private readonly http: HttpClient,
    private readonly entityPreviewMapper: EntityPreviewMapper,
  ) { }

  public getEntityPreview(entityName: string): Observable<EntityPreview[]> {
    const entityItems$ = this.http.get<EntityPreviewDto[]>(`${API_FORM_ENTITY_URI}${entityName}`);

    return entityItems$.pipe(
      map(data => data.map(item => this.entityPreviewMapper.fromDto(item))),
    );
  }
}
