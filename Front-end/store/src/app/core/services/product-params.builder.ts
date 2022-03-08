import { Injectable, Input } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';

import { PaginationOptions } from '../interfaces/pagination-options';

import { FilterOptions } from '../interfaces/filter-options';
import { SearchOptions } from '../interfaces/search-options';
import { SortingOptions } from '../interfaces/sorting-options';
import { PAGE_SIZE } from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class ProductParamsBuilder {

  private params: HttpParams = new HttpParams();

  constructor() {
  }

  public addPaging(paginationOptions: PaginationOptions): ProductParamsBuilder {
    const { pageNumber, pageSize } = paginationOptions;

    this.params = this.params.set('pageSize', pageSize);
    this.params = this.params.set('pageNumber', pageNumber);

    return this;
  }

  public addFiltration(filterOptions: FilterOptions): ProductParamsBuilder {

    const filterValues = filterOptions.values;

    Object.entries(filterValues).map(([propertyName, value], index) => {
      this.params = this.params.set(`filters[${index}].propertyName`, propertyName);
      this.params = this.params.set(`filters[${index}].comparedValue`, value);
    });

    return this;
  }

  public addSortings(sortingOptions: SortingOptions[]): ProductParamsBuilder {

    for (let i = 0; i < sortingOptions.length; i++) {
      const { propertyName, isAscending } = sortingOptions[i];

      this.params = this.params.set(`sortings[${i}].propertyName`, propertyName);

      if (isAscending !== undefined) {
        this.params = this.params.set(`sortings[${i}].isAscending`, isAscending);
      }
    }

    return this;
  }

  public addSearch(searchOptions: SearchOptions): ProductParamsBuilder {
    const { propertyName, value, searchDepth } = searchOptions;

    if (propertyName && value && searchDepth) {
      this.params = this.params.set('search.propertyName', propertyName);
      this.params = this.params.set(`search.comparedValue`, value);
      this.params = this.params.set(`search.searchDepth`, searchDepth);
    }

    return this;
  }

  public build(): HttpParams {
    const builtParams = this.params;

    this.params = new HttpParams();

    return builtParams;
  }
}
