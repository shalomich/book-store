import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {SearchHintsDto} from "../DTOs/search-hints-dto";
import * as http from "http";
import {PRODUCT_URL} from "../utils/values";
import {Injectable} from "@angular/core";
import {ProductParamsBuilder} from "./product-params.builder";
import {SearchOptions} from "../interfaces/search-options";
import {PaginationOptions} from "../interfaces/pagination-options";

@Injectable({
  providedIn: 'root',
})
export class SearchService {

  public constructor(
    private readonly http: HttpClient,
    private readonly paramsBuilder: ProductParamsBuilder) { }

  public getHints(searchOptions: SearchOptions, paginationOptions: PaginationOptions): Observable<SearchHintsDto> {
    const params = this.paramsBuilder
      .addSearch(searchOptions)
      .addPaging(paginationOptions)
      .build();

    return this.http.get<SearchHintsDto>(`${PRODUCT_URL}book/hint`, { params });
  }
}
