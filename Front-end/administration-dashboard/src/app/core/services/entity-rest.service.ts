import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { API_FORM_ENTITY_URI } from '../utils/values';
import {EntityDto} from "../DTOs/entity-dto";
import { ProductTypeConfigurationService } from './product-type-configuration.service';

@Injectable({
  providedIn: 'root',
})
export class EntityRestService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  public constructor(private readonly http: HttpClient, private readonly productTypeService: ProductTypeConfigurationService ) { }

  private checkEntityType(entityType:string) {
    if (!this.productTypeService.isEntity(entityType))
      throw "Can't continue operation with no entity type"
  }

  public getById(entityType: string, itemId: number): Observable<EntityDto> {
    this.checkEntityType(entityType);
    this.productTypeService.isEntity(entityType)
    return this.http.get<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${itemId}`);
  }

  public get(entityType: string): Observable<EntityDto[]> {
    this.checkEntityType(entityType);
    return this.http.get<EntityDto[]>(`${API_FORM_ENTITY_URI}${entityType}`);
  }

  public add(entityType: string, data: EntityDto): Observable<void> {
    this.checkEntityType(entityType);
    this.http.post<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}`, data, this.httpOptions).subscribe();

    return of();
  }

  public edit(entityType: string, id: number, data: EntityDto): Observable<void> {
    this.checkEntityType(entityType);
    this.http.put<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${id}`, data, this.httpOptions).subscribe();

    return of();
  }

  public delete(entityType: string, id: number): Observable<void> {
    this.checkEntityType(entityType);
    this.http.delete<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${id}`, this.httpOptions).subscribe();

    return of();
  }
}
