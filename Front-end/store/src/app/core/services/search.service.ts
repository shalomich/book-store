import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {SearchHintsDto} from "../DTOs/search-hints-dto";
import * as http from "http";
import {PRODUCT_URL} from "../utils/values";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root',
})
export class SearchService {

  public constructor(
    private readonly http: HttpClient) { }

  public getHints(params: HttpParams): Observable<SearchHintsDto> {
    return this.http.get<SearchHintsDto>(`${PRODUCT_URL}book/hint`, { params });
  }
}
