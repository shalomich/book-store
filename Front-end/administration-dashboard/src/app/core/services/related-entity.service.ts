import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class RelatedEntityService extends EntityService {

  public constructor(
    http: HttpClient,
    private readonly relatedEntityMapper: RelatedEntityMapper,
  ) {
    super(http);
  }

  public getSingleItem(relatedEntityType: string, itemId: number): Observable<RelatedEntity> {
    const entityItem$ = super.getSingleEntityItem<RelatedEntityDto>(relatedEntityType, itemId);

    return entityItem$.pipe(
      map(item => this.relatedEntityMapper.fromDto(item)),
    );
  }

  public getItems(relatedEntityType: string, idsArray?: number[]): Observable<RelatedEntity[]> {
    const entityItems$ = super.getAllEntityItems<RelatedEntityDto>(relatedEntityType);

    if (idsArray) {
      return entityItems$.pipe(
        map(data => data.filter(item => idsArray.find(num => num === item.id) !== undefined)),
      );
    }
    return entityItems$;
  }
}
