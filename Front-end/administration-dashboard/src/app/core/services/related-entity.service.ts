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
export class RelatedEntityService {

  public constructor(
    private readonly http: HttpClient,
    private readonly relatedEntityMapper: RelatedEntityMapper,
    private readonly entityService: EntityService,
  ) { }

  public addRelatedEntityItem(dataToAdd: RelatedEntity, entityName: string): Observable<void> {
    const relatedEntityItem = this.relatedEntityMapper.toDto(dataToAdd);
    relatedEntityItem.id = 0;

    return this.entityService.add<RelatedEntityDto>(entityName, relatedEntityItem);
  }

  public editRelatedEntityItem(dataToEdit: RelatedEntity, entityName: string): Observable<void> {
    const relatedEntityItem = this.relatedEntityMapper.toDto(dataToEdit);

    return this.entityService.edit<RelatedEntityDto>(entityName, relatedEntityItem.id, relatedEntityItem);
  }

  public getSingleItem(relatedEntityType: string, itemId: number): Observable<RelatedEntity> {
    const entityItem$ = this.entityService.getById<RelatedEntityDto>(relatedEntityType, itemId);

    return entityItem$.pipe(
      map(item => this.relatedEntityMapper.fromDto(item)),
    );
  }

  public getItems(relatedEntityType: string, idsArray?: number[]): Observable<RelatedEntity[]> {
    const entityItems$ = this.entityService.getAll<RelatedEntityDto>(relatedEntityType);

    if (idsArray) {
      return entityItems$.pipe(
        map(data => data.filter(item => idsArray.find(num => num === item.id) !== undefined)),
      );
    }
    return entityItems$;
  }
}
