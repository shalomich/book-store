import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EntityParamsBuilder {

  private _params = new HttpParams();

  public get params() {
    return this._params;
  }

  public setPagging(pageSize: number, pageNumber?: number): void{
    this._params = this._params.set('pagging.pageSize',pageSize);

    if (pageNumber)
      this._params = this._params.set('pagging.pageNumber', pageNumber);
  }

}
