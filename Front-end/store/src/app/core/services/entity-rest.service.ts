import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {BookMapper} from "../mappers/book.mapper";
import {ProductPreviewMapper} from "../mappers/product-preview.mapper";
import {RelatedEntityMapper} from "../mappers/related-entity.mapper";
import {EntityDto} from "../DTOs/entity-dto";
import {Router} from "@angular/router";
import {PRODUCT_URL} from "../utils/values";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class EntityRestService {

  public constructor(
    private readonly http: HttpClient) { }

  public get(productName: string, relatedEntityName?: string, params?: HttpParams): Observable<EntityDto[]> {
    let entityUri: string = `${PRODUCT_URL}${productName}`;

    if (relatedEntityName)
      entityUri = `${entityUri}/${relatedEntityName}`

    return this.http.get<EntityDto[]>(entityUri, { params });
  }
}
