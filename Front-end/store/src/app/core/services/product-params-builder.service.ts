import { Injectable, Input } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';

import { PaginationOptions } from '../interfaces/pagination-options';
import { FilterOptions } from '../interfaces/filter-options';
import { PAGE_SIZE } from '../utils/values';
import { SearchOptions } from '../interfaces/search-options';

@Injectable({
  providedIn: 'root',
})
export class ProductParamsBuilderService {

  public paginationOptions$: BehaviorSubject<PaginationOptions> = new BehaviorSubject<PaginationOptions>({
    pageNumber: 1,
    pageSize: PAGE_SIZE,
  });

  public pageCount$: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  public filterOptions$: BehaviorSubject<FilterOptions> = new BehaviorSubject<FilterOptions>(
    {
      values: {},
    },
  );

  public searchOptions$: BehaviorSubject<SearchOptions> = new BehaviorSubject<SearchOptions>(
    {
      propertyName: '',
      value: '',
      searchDepth: 0,
    },
  );

  public onParamsChanged: (params: HttpParams) => void = params => {};

  public changePageCount: (params: HttpParams) => Observable<number> = params => new Observable<number>();

  constructor() {
    this.filterOptions$.asObservable()
      .subscribe(options => {
        this.resetPaging();

        const params = this.buildParams();
        this.changePageCount(params)
          .subscribe(pageCount => this.pageCount$.next(pageCount));
      });

    this.paginationOptions$.asObservable()
      .subscribe(options => {
        this.onParamsChanged(this.buildParams());
      });

    this.searchOptions$.asObservable()
      .subscribe(options => {
        this.resetPaging();

        const params = this.buildParams();
        this.changePageCount(params)
          .subscribe(pageCount => this.pageCount$.next(pageCount));
      });
  }

  private resetPaging(): void {
    const { pageSize } = this.paginationOptions$.value;

    this.paginationOptions$.next(
      {
        pageSize,
        pageNumber: 1,
      },
    );
  }

  private buildParams(): HttpParams {
    let params = new HttpParams();

    params = this.buildFilters(params);
    params = this.buildSearch(params);

    return this.buildPaging(params);
  }

  private buildPaging(params: HttpParams): HttpParams {
    const { pageNumber, pageSize } = this.paginationOptions$.value;

    params = params.set('pageSize', pageSize);
    params = params.set('pageNumber', pageNumber);

    return params;
  }

  private buildFilters(params: HttpParams): HttpParams {

    const filterValues = this.filterOptions$.value.values;

    Object.entries(filterValues).map(([propertyName, value], index) => {
      params = params.set(`filters[${index}].propertyName`, propertyName);
      params = params.set(`filters[${index}].comparedValue`, value);
    });

    return params;
  }

  private buildSearch(params: HttpParams): HttpParams {
    return params;
  }
}
