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
}
