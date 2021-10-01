import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { ProductPreview } from '../models/product-preview';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityRestService } from './entity-rest.service';
import {RelatedEntityPreviewMapper} from "../mappers/related-entity-preview.mapper";
import {RelatedEntityPreview} from "../models/related-entity-preview";
import {ProductDto} from "../DTOs/product-dto";

@Injectable({
  providedIn: 'root',
})
export class EntityPreviewService {

  public constructor(
    private readonly http: HttpClient,
    private readonly productPreviewMapper: ProductPreviewMapper,
    private readonly relatedEntityPreviewMapper: RelatedEntityPreviewMapper,
    private readonly entityService: EntityRestService,
  ) { }

  public getProductPreviews(productType: string): Observable<ProductPreview[]> {

    return this.entityService.get(productType).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.productPreviewMapper.fromDto(entityDto as ProductDto))),
    );
  }

  public getRelatedEntityPreviews(relatedEntityType: string): Observable<RelatedEntityPreview[]> {

    return this.entityService.get(relatedEntityType).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.relatedEntityPreviewMapper.fromDto(entityDto as RelatedEntityDto))),
    );
  }
}
