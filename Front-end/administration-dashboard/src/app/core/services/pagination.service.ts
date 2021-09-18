import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { ProductPreview } from '../models/product-preview';
import { ProductPreviewDto } from '../DTOs/product-preview-dto';


import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class PaginationService extends EntityService {

  public constructor(
    http: HttpClient,
    private readonly productPreviewMapper: ProductPreviewMapper,
    private readonly relatedEntityMapper: RelatedEntityMapper,
  ) {
    super(http);
  }

  public getProductPage(productType: string): Observable<ProductPreview[]> {
    const entityItems$ = super.getEntityPage<ProductPreviewDto>(productType);

    return entityItems$.pipe(
      map(data => data.map(item => this.productPreviewMapper.fromDto(item))),
    );
  }

  public getRelatedEntityPage(relatedEntityType: string): Observable<RelatedEntity[]> {
    const entityItems$ = super.getEntityPage<RelatedEntityDto>(relatedEntityType);

    return entityItems$.pipe(
      map(data => data.map(item => this.relatedEntityMapper.fromDto(item))),
    );
  }
}
