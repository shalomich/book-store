import { Injectable, Input } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { map } from 'rxjs/operators';

import { PaginationOptions } from '../interfaces/pagination-options';

import { FilterOptions } from '../interfaces/filter-options';
import { SearchOptions } from '../interfaces/search-options';
import { SortingOptions } from '../interfaces/sorting-options';
import { PAGE_SIZE } from '../utils/values';
import { OptionGroup } from '../interfaces/option-group';

@Injectable({
  providedIn: 'root',
})
export class ProductOptionsStorage {
  public constructor() {
  }

  private optionGroupSubject$: BehaviorSubject<OptionGroup> = new BehaviorSubject<OptionGroup>({
    pagingOptions: {
      pageNumber: 1,
      pageSize: 1,
    },
    sortingOptions: [],
    filterOptions: null,
  });

  public get optionGroup$() {
    return this.optionGroupSubject$.asObservable();
  }

  public setPaginationOptions(pagingOptions: PaginationOptions) {
    this.optionGroupSubject$.next({
      ...this.optionGroupSubject$.value,
      pagingOptions,
    });
  }

  public setFilterOptions(filterOptions: FilterOptions) {
    this.optionGroupSubject$.next({
      ...this.optionGroupSubject$.value,
      filterOptions,
      pagingOptions: {
        pageNumber: 1,
        pageSize: this.optionGroupSubject$.value.pagingOptions.pageSize
      }
    });
  }

  public setSortingOptions(sortingOptions: SortingOptions[]) {
    this.optionGroupSubject$.next({
      ...this.optionGroupSubject$.value,
      sortingOptions,
      pagingOptions: {
        pageNumber: 1,
        pageSize: this.optionGroupSubject$.value.pagingOptions.pageSize
      }
    });
  }
}
