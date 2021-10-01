import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { API_FORM_ENTITY_URI } from '../utils/values';
import {EntityDto} from "../DTOs/entity-dto";

@Injectable({
  providedIn: 'root',
})
export class EntityRestService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  public constructor(private readonly http: HttpClient) { }

  public getById(entityType: string, itemId: number): Observable<EntityDto> {
    return this.http.get<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${itemId}`);
  }

  public get(entityType: string): Observable<EntityDto[]> {
    return this.http.get<EntityDto[]>(`${API_FORM_ENTITY_URI}${entityType}`);
  }

  public add(entityType: string, data: EntityDto): Observable<void> {
    this.http.post<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}`, data, this.httpOptions).subscribe();

    return of();
  }

  public edit(entityType: string, id: number, data: EntityDto): Observable<void> {
    this.http.put<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${id}`, data, this.httpOptions).subscribe();

    return of();
  }

  public delete(entityType: string, id: number): Observable<void> {
    this.http.delete<EntityDto>(`${API_FORM_ENTITY_URI}${entityType}/${id}`, this.httpOptions).subscribe();

    return of();
  }
}
