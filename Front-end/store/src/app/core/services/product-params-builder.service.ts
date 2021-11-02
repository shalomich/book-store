import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { PaginationOptions } from '../interfaces/pagination-options';
import { Comparison } from '../utils/comparison';
import { FilterOptions } from '../interfaces/filter-options';

@Injectable({
  providedIn: 'root',
})
export class ProductParamsBuilderService {

  private readonly filterProperties: Array<[string, Comparison]> = new Array<[string, Comparison]>();

  private _params = new HttpParams();

  public get params() {
    return this._params;
  }

  public setPaging(paginationOptions: PaginationOptions): ProductParamsBuilderService {
    this._params = this._params.set('pageSize', paginationOptions.pageSize);
    this._params = this._params.set('pageNumber', paginationOptions.pageNumber);

    return this;
  }

  public addFilter(filerParams: FilterOptions) {
    const { propertyName, value, comparison } = filerParams;

    const filterDataPredicate = (filterData: [string, Comparison]): boolean => {
      const [filteredPropertyName, filteredComparison] = filterData;
      return filteredPropertyName === propertyName && filteredComparison === comparison;
    };

    if (!this.filterProperties.find(filterDataPredicate)) {
      this.filterProperties.push([propertyName, comparison]);
    }

    const filterNumber = this.filterProperties.findIndex(filterDataPredicate);

    const propertyNameParamName = `filters[${filterNumber}].propertyName`;
    const valueParamName = `filters[${filterNumber}].comparedValue`;
    const comparisonParamName = `filters[${filterNumber}].comparison`;

    if (value) {
      this._params = this._params.set(propertyNameParamName, propertyName);
      this._params = this._params.set(valueParamName, value);
      this._params = this._params.set(comparisonParamName, comparison);
    } else {
      this._params = this._params.delete(propertyNameParamName);
      this._params = this._params.delete(valueParamName);
      this._params = this._params.delete(comparisonParamName);
    }
  }
}
