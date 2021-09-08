import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { EntityPreview } from '../models/entity-preview';
import { EntityPreviewDto } from '../DTOs/entity-preview-dto';
import { EntityPreviewMapper } from '../mappers/entity-preview.mapper';
import {Response} from '../interfaces/response';

@Injectable({
  providedIn: 'root',
})
export class EntityPreviewService {

  constructor(
    private readonly http: HttpClient,
    private readonly entityPreviewMapper: EntityPreviewMapper,
  ) { }

  public getEntityPreview(entityName: string): Observable<EntityPreview[]> {
    const entityItems$ = this.http.get<Response>(`https://localhost:44327/dashboard/form-entity/${entityName}`);

    return entityItems$.pipe(
      map(data => data.formEntityIdentities.map(item => this.entityPreviewMapper.fromDto(item))),
    );
  }
}
