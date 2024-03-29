import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import {map, share, shareReplay} from 'rxjs/operators';

import { Observable } from 'rxjs';

import { BookDto } from '../DTOs/book-dto';
import { FILTERS_URL, PRODUCT_URL } from '../utils/values';
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

import { BookFilters } from '../interfaces/book-filters';

import { ProductParamsBuilder } from './product-params.builder';
import { AuthorizationDataProvider } from './authorization-data.provider';
import { AuthorizationService } from './authorization.service';

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
    private readonly authorizationService: AuthorizationService,
  ) { }

  public getById(id: number) {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const options = this.authorizationService.accessToken ? { headers } : {};

    const book$ = this.http.get<BookDto>(`${PRODUCT_URL}/${id}`, options);

    return book$.pipe(
      map(book => this.bookMapper.fromDto(book)),
    );
  }

  public addBookView(id: number): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.post<void>(`${PRODUCT_URL}/${id}/view`, {}, { headers });
  }

  public get(optionGroup: OptionGroup): Observable<ProductPreviewSet> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const { pagingOptions, filterOptions, sortingOptions } = optionGroup;

    this.paramsBuilder.addPaging(pagingOptions);

    if (filterOptions) {
      this.paramsBuilder.addFiltration(filterOptions);
    }

    if (sortingOptions) {
      this.paramsBuilder.addSortings(sortingOptions);
    }

    const params = this.paramsBuilder.build();

    const options = this.authorizationService.accessToken ? { headers, params } : { params };

    return this.http.get<ProductPreviewSetDto>(`${PRODUCT_URL}`, options).pipe(
      map(setDto => this.productPreviewSetMapper.fromDto(setDto)),
    );
  }

  public getFilters(): Observable<BookFilters> {
    return this.http.get<BookFilters>(`${FILTERS_URL}`).pipe(shareReplay({ bufferSize: 1, refCount: true }));
  }
}
