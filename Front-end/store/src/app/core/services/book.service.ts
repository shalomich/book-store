import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Observable } from 'rxjs';

import { BookDto } from '../DTOs/book-dto';
import { PRODUCT_URL } from '../utils/values';
import { BookMapper } from '../mappers/book.mapper';
import { ProductPreview } from '../models/product-preview';
import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntity } from '../models/related-entity';
import { ProductPreviewSetMapper } from '../mappers/product-preview-set.mapper';
import { ProductPreviewSet } from '../models/product-preview-set';
import { ProductPreviewSetDto } from '../DTOs/product-preview-set-dto';
import { OptionGroup } from '../interfaces/option-group';

import { SearchOptions } from '../interfaces/search-options';

import { ProductParamsBuilder } from './product-params.builder';

@Injectable({
  providedIn: 'root',
})
export class BookService {

  private readonly type = 'book';

  public constructor(
    private readonly http: HttpClient,
    private readonly bookMapper: BookMapper,
    private readonly productPreviewSetMapper: ProductPreviewSetMapper,
    private readonly relatedEntityMapper: RelatedEntityMapper,
    private readonly paramsBuilder: ProductParamsBuilder,
  ) { }

  public getById(id: number) {
    const book$ = this.http.get<BookDto>(`${PRODUCT_URL}${this.type}/${id}`);

    return book$.pipe(
      map(book => this.bookMapper.fromDto(book)),
    );
  }

  public get(optionGroup: OptionGroup, searchOptions?: SearchOptions): Observable<ProductPreviewSet> {

    const { pagingOptions, filterOptions, sortingOptions } = optionGroup;

    this.paramsBuilder.addPaging(pagingOptions);

    if (filterOptions) {
      this.paramsBuilder.addFiltration(filterOptions);
    }

    if (sortingOptions) {
      this.paramsBuilder.addSortings(sortingOptions);
    }

    if (searchOptions) {
      this.paramsBuilder.addSearch(searchOptions);
    }

    const params = this.paramsBuilder.build();

    return this.http.get<ProductPreviewSetDto>(`${PRODUCT_URL}${this.type}`, { params }).pipe(
      map(setDto => this.productPreviewSetMapper.fromDto(setDto)),
    );
  }

  public getRelatedEntity(entityName: string): Observable<RelatedEntity[]> {
    return this.http.get<RelatedEntityDto[]>(`${PRODUCT_URL}${this.type}/${entityName}`)
      .pipe(map(items => items.map(item => this.relatedEntityMapper.fromDto(item))));
  }
}
