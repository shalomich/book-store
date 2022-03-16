import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Observable } from 'rxjs';

import { BookDto } from '../DTOs/book-dto';
import { PRODUCT_URL, SELECTION_URL } from '../utils/values';
import { BookMapper } from '../mappers/book.mapper';
import { ProductPreview } from '../models/product-preview';
import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntity } from '../models/related-entity';
import { Selection } from '../enums/selection';
import { ProductPreviewSetMapper } from '../mappers/product-preview-set.mapper';
import { ProductPreviewSetDto } from '../DTOs/product-preview-set-dto';
import { ProductPreviewSet } from '../models/product-preview-set';

import { PaginationOptions } from '../interfaces/pagination-options';
import { SortingOptions } from '../interfaces/sorting-options';
import { FilterOptions } from '../interfaces/filter-options';
import { OptionGroup } from '../interfaces/option-group';

import { ProductParamsBuilder } from './product-params.builder';
import {AuthorizationDataProvider} from './authorization-data.provider';

@Injectable({
  providedIn: 'root',
})
export class SelectionService {

  public constructor(
    private readonly http: HttpClient,
    private readonly productPreviewSetMapper: ProductPreviewSetMapper,
    private readonly paramsBuilder: ProductParamsBuilder,
    private readonly authorizationDataProvider: AuthorizationDataProvider,
  ) { }

  public get(selection: Selection, optionGroup: OptionGroup): Observable<ProductPreviewSet> {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
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

    return this.http.get<ProductPreviewSetDto>(`${SELECTION_URL}${selection}`, { params, headers }).pipe(
      map(setDto => this.productPreviewSetMapper.fromDto(setDto)),
    );
  }

}
