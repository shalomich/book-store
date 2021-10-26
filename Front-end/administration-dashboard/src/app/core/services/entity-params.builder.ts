import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {FilterParams} from "../interfaces/filter-params";
import {Comparison} from "../utils/comparison";

@Injectable({
  providedIn: 'root'
})
export class EntityParamsBuilder {

  private readonly filterProperties: Array<[string,Comparison]> = new Array<[string, Comparison]>();

  private _params = new HttpParams();

  public get params() {
    return this._params;
  }

  public addFilter(filerParams: FilterParams) {
    const {propertyName, value, comparison} = filerParams;

    const filterDataPredicate = (filterData: [string, Comparison]) => {
      const [filteredPropertyName, filteredComparison] = filterData;
      return filteredPropertyName == propertyName && filteredComparison == comparison;
    }

    if (!this.filterProperties.find(filterDataPredicate))
      this.filterProperties.push([propertyName, comparison]);

    const filterNumber = this.filterProperties.findIndex(filterDataPredicate);

    const propertyNameParamName = `filters[${filterNumber}].propertyName`;
    const valueParamName = `filters[${filterNumber}].comparedValue`;
    const comparisonParamName = `filters[${filterNumber}].comparison`;

    if (value) {
      this._params = this._params.set(propertyNameParamName, propertyName);
      this._params = this._params.set(valueParamName, value);
      this._params = this._params.set(comparisonParamName, comparison);
    }
    else {
      this._params = this._params.delete(propertyNameParamName);
      this._params = this._params.delete(valueParamName);
      this._params = this._params.delete(comparisonParamName);
    }
  }
}
