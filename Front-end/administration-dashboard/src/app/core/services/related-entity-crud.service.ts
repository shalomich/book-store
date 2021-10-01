import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityRestService } from './entity-rest.service';
import { BookDto } from '../DTOs/book-dto';
import {RelatedEntityPreview} from "../models/related-entity-preview";

@Injectable({
  providedIn: 'root',
})
export class RelatedEntityCrudService {

  public constructor(
    private readonly http: HttpClient,
    private readonly relatedEntityMapper: RelatedEntityMapper,
    private readonly entityService: EntityRestService,
  ) { }

  public add(entityType: string, relatedEntity: RelatedEntity): Observable<void> {
    const relatedEntityDto = this.relatedEntityMapper.toDto(relatedEntity);
    relatedEntityDto.id = 0;

    return this.entityService.add(entityType, relatedEntityDto);
  }

  public edit(entityType: string, relatedEntity: RelatedEntity): Observable<void> {
    const relatedEntityDto = this.relatedEntityMapper.toDto(relatedEntity);

    return this.entityService.edit(entityType, relatedEntityDto.id, relatedEntityDto);
  }

  public delete(entityType: string, id: number): Observable<void> {
    return this.entityService.delete(entityType, id);
  }

  public getById(entityType: string, id: number): Observable<RelatedEntity> {
    return this.entityService.getById(entityType, id)
      .pipe(
        map(entityDto => this.relatedEntityMapper.fromDto(entityDto as RelatedEntityDto)),
      );
  }

  public get(entityType: string): Observable<RelatedEntity[]> {

    return this.entityService.get(entityType).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.relatedEntityMapper.fromDto(entityDto as RelatedEntityDto))),
    );
  }
}
