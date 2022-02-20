import { Injectable, Input } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import {BehaviorSubject, Observable, Subject} from 'rxjs';

import { PaginationOptions } from '../interfaces/pagination-options';

import { FilterOptions } from '../interfaces/filter-options';
import { SearchOptions } from '../interfaces/search-options';
import { SortingOptions } from '../interfaces/sorting-options';
import { PAGE_SIZE } from '../utils/values';
import {OptionGroup} from "../interfaces/option-group";

@Injectable({
  providedIn: 'root',
})
export class ProductOptionsStorage {

  private optionGroup: OptionGroup = {
    pagingOptions: {
      pageNumber: 1,
      pageSize: 1
    },
    sortingOptions: [],
    filterOptions: undefined,
  }

  private optionGroupSubject: Subject<OptionGroup> = new Subject<OptionGroup>();

  public get optionGroup$() {
    return this.optionGroupSubject.asObservable();
  }

  public setPaginationOptions(pagingOptions: PaginationOptions) {
    this.optionGroup.pagingOptions = pagingOptions;
    this.optionGroupSubject.next(this.optionGroup);
  }

  public setFilterOptions(filterOptions: FilterOptions) {
    this.optionGroup.filterOptions = filterOptions;
    this.resetPaging();
    this.optionGroupSubject.next(this.optionGroup);
  }

  public setSortingOptions(sortingOptions: SortingOptions[]) {
    this.optionGroup.sortingOptions = sortingOptions;
    this.resetPaging();
    this.optionGroupSubject.next(this.optionGroup);
  }

  private resetPaging(): void {
    this.optionGroup.pagingOptions = {
      pageNumber: 1,
      pageSize: this.optionGroup.pagingOptions.pageSize
    }
  }
}
