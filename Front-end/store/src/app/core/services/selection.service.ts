import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Observable } from 'rxjs';

import { BookDto } from '../DTOs/book-dto';
import {PRODUCT_URL, SELECTION_URL} from '../utils/values';
import { BookMapper } from '../mappers/book.mapper';
import { ProductPreview } from '../models/product-preview';
import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntity } from '../models/related-entity';

@Injectable({
  providedIn: 'root',
})
export class SelectionService {

  public constructor(
    private readonly http: HttpClient,
    private readonly productPreviewMapper: ProductPreviewMapper
  ) { }

  public get(selectionName: string, params?: HttpParams): Observable<ProductPreview[]> {
    return this.http.get<ProductPreviewDto[]>(`${SELECTION_URL}${selectionName}`, { params }).pipe(
      map(product => product.map(product => this.productPreviewMapper.fromDto(product))),
    );
  }
}
