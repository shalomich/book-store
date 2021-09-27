import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { API_FORM_ENTITY_URI } from '../utils/values';

@Injectable()
export abstract class EntityService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  protected constructor(private readonly http: HttpClient) { }

  protected getEntityPage<T>(entityName: string): Observable<T[]> {
    return this.http.get<T[]>(`${API_FORM_ENTITY_URI}${entityName}`);
  }

  protected getSingleEntityItem<T>(entityName: string, itemId: number): Observable<T> {
    return this.http.get<T>(`${API_FORM_ENTITY_URI}${entityName}/${itemId}`);
  }

  protected getAllEntityItems<T>(entityName: string): Observable<T[]> {
    return this.http.get<T[]>(`${API_FORM_ENTITY_URI}${entityName}`);
  }

  protected addEntityItem<T>(entityName: string, data: T): Observable<void> {
    this.http.post<T>(`${API_FORM_ENTITY_URI}${entityName}`, data, this.httpOptions);

    return of();
  }

  protected editEntityItem<T>(entityName: string, id: number, data: T): Observable<void> {
    this.http.put<T>(`${API_FORM_ENTITY_URI}${entityName}/${id}`, data, this.httpOptions)
      .subscribe();

    return of();
  }
}
