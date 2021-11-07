import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';

import { PaginationOptions } from '../interfaces/pagination-options';

@Injectable({
  providedIn: 'root',
})
export class ProductParamsBuilderService {

  private _params = new HttpParams();

  public get params() {
    return this._params;
  }

  public setPaging(paginationOptions: PaginationOptions): ProductParamsBuilderService {
    this._params = this._params.set('pageSize', paginationOptions.pageSize);
    this._params = this._params.set('pageNumber', paginationOptions.pageNumber);

    return this;
  }

  public setFilter(filerParams: Map<string, string>) {
    console.log(filerParams);
    const options = Array.from(filerParams, ([name, value]) => ({ name, value }));

    options.forEach((option, index) => {
      const propertyNameParamName = `filters[${index}].propertyName`;
      const valueParamName = `filters[${index}].comparedValue`;
      this._params = this._params.set(propertyNameParamName, option.name);
      this._params = this._params.set(valueParamName, option.value);
    });
  }
}
