import * as http from 'http';

import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';

import { map } from 'rxjs/operators';

import { SearchHintsDto } from '../DTOs/search-hints-dto';
import { PRODUCT_URL, SEARCH_URL } from '../utils/values';


import { SearchOptions } from '../interfaces/search-options';

import { PaginationOptions } from '../interfaces/pagination-options';

import { OptionGroup } from '../interfaces/option-group';
import { ProductPreviewSet } from '../models/product-preview-set';
import { ProductPreviewSetDto } from '../DTOs/product-preview-set-dto';


import { ProductPreviewSetMapper } from '../mappers/product-preview-set.mapper';

import { ProductParamsBuilder } from './product-params.builder';

@Injectable({
  providedIn: 'root',
})
export class SearchService {

  public constructor(
    private readonly http: HttpClient,
    private readonly paramsBuilder: ProductParamsBuilder,
    private readonly productPreviewSetMapper: ProductPreviewSetMapper,
  ) { }

  public getHints(searchOptions: SearchOptions, paginationOptions: PaginationOptions): Observable<SearchHintsDto> {
    const params = this.paramsBuilder
      .addSearch(searchOptions)
      .addPaging(paginationOptions)
      .build();

    return this.http.get<SearchHintsDto>(`${SEARCH_URL}/hint`, { params });
  }

  public get(optionGroup: OptionGroup, searchOptions: SearchOptions): Observable<ProductPreviewSet> {
    const { pagingOptions, filterOptions, sortingOptions } = optionGroup;

    this.paramsBuilder.addPaging(pagingOptions);

    if (filterOptions) {
      this.paramsBuilder.addFiltration(filterOptions);
    }

    if (sortingOptions) {
      this.paramsBuilder.addSortings(sortingOptions);
    }

    this.paramsBuilder.addSearch(searchOptions);

    const params = this.paramsBuilder.build();

    return this.http.get<ProductPreviewSetDto>(`${SEARCH_URL}`, { params }).pipe(
      map(setDto => this.productPreviewSetMapper.fromDto(setDto)),
    );
  }
}
