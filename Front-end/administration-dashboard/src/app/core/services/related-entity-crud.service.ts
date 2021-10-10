import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { BookDto } from '../DTOs/book-dto';
import { RelatedEntityPreview } from '../models/related-entity-preview';

import { EntityRestService } from './entity-rest.service';
import { ProductTypeConfigurationService } from './product-type-configuration.service';

@Injectable({
  providedIn: 'root',
})
export class RelatedEntityCrudService {

  public constructor(
    private readonly http: HttpClient,
    private readonly relatedEntityMapper: RelatedEntityMapper,
    private readonly entityService: EntityRestService,
    private readonly productTypeService: ProductTypeConfigurationService,
  ) { }

  private checkRelatedEntityType(entityType: string) {
    if (!this.productTypeService.isRelatedEntity(entityType)) {
      throw 'Can\'t continue operation with no related entity type';
    }
  }

  public add(entityType: string, relatedEntity: RelatedEntity): Observable<void> {
    this.checkRelatedEntityType(entityType);
    const relatedEntityDto = this.relatedEntityMapper.toDto(relatedEntity);
    relatedEntityDto.id = 0;

    return this.entityService.add(entityType, relatedEntityDto);
  }

  public edit(entityType: string, relatedEntity: RelatedEntity): Observable<void> {
    this.checkRelatedEntityType(entityType);
    const relatedEntityDto = this.relatedEntityMapper.toDto(relatedEntity);

    return this.entityService.edit(entityType, relatedEntityDto.id, relatedEntityDto);
  }

  public delete(entityType: string, id: number): Observable<void> {
    this.checkRelatedEntityType(entityType);
    return this.entityService.delete(entityType, id);
  }

  public getById(entityType: string, id: number): Observable<RelatedEntity> {
    this.checkRelatedEntityType(entityType);
    return this.entityService.getById(entityType, id)
      .pipe(
        map(entityDto => this.relatedEntityMapper.fromDto(entityDto as RelatedEntityDto)),
      );
  }

  public get(entityType: string): Observable<RelatedEntity[]> {
    this.checkRelatedEntityType(entityType);
    return this.entityService.get(entityType).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.relatedEntityMapper.fromDto(entityDto as RelatedEntityDto))),
    );
  }
}
