import { Injectable } from '@angular/core';

import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { ProductPreview } from '../models/product-preview';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { RelatedEntityPreviewMapper } from '../mappers/related-entity-preview.mapper';
import { RelatedEntityPreview } from '../models/related-entity-preview';
import { ProductDto } from '../DTOs/product-dto';

import { EntityRestService } from './entity-rest.service';
import {ProductTypeConfigurationService} from "./product-type-configuration.service";

@Injectable({
  providedIn: 'root',
})
export class EntityPreviewService {

  public constructor(
    private readonly productPreviewMapper: ProductPreviewMapper,
    private readonly relatedEntityPreviewMapper: RelatedEntityPreviewMapper,
    private readonly entityService: EntityRestService,
    private readonly productTypeService: ProductTypeConfigurationService
  ) { }

  public getProductPreviews(productType: string, params?: HttpParams): Observable<ProductPreview[]> {
    if (!this.productTypeService.isProduct(productType))
      throw "Can't continue operation with no product type";

    return this.entityService.get(productType,params).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.productPreviewMapper.fromDto(entityDto as ProductDto))),
    );
  }

  public getRelatedEntityPreviews(relatedEntityType: string, params?: HttpParams): Observable<RelatedEntityPreview[]> {
    if (!this.productTypeService.isRelatedEntity(relatedEntityType))
      throw "Can't continue operation with no related entity type";

    return this.entityService.get(relatedEntityType, params).pipe(
      map(entityDtos => entityDtos.map(entityDto => this.relatedEntityPreviewMapper.fromDto(entityDto as RelatedEntityDto))),
    );
  }

  public getPreviewPageCount(entityType: string, params?: HttpParams): Observable<number> {
    return this.entityService.getPageCount(entityType, params);
  }
}
