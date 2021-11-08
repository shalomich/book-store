import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { PaginationOptions } from '../interfaces/pagination-options';
import { FilterOptions } from '../interfaces/filter-options';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class ProductParamsBuilderService {

  private _params = new HttpParams();

  public filterOptions$: BehaviorSubject<FilterOptions> = new BehaviorSubject<FilterOptions>(
  {
    values: {}
  });

  public onParamsChanged: ((params: HttpParams) => void) = params => {};

  constructor() {
    this.filterOptions$.asObservable()
      .subscribe( options =>
      {
          this.onParamsChanged(this.buildParams());
      });
  }

  private buildParams(): HttpParams {
    return this.buildFilters(this._params);
  }

  public get params() {
    return this._params;
  }

  public setPaging(paginationOptions: PaginationOptions): ProductParamsBuilderService {
    this._params = this._params.set('pageSize', paginationOptions.pageSize);
    this._params = this._params.set('pageNumber', paginationOptions.pageNumber);

    return this;
  }

  public buildFilters(params: HttpParams): HttpParams {

    const filterValues = this.filterOptions$.value.values;

    Object.entries(filterValues).map(([propertyName, value], index) =>
    {
      params = params.set(`filters[${index}].propertyName`, propertyName);
      params = params.set(`filters[${index}].comparedValue`, value);
    })

    return params;
  }
}
