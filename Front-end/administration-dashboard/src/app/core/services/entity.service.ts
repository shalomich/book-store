import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { API_FORM_ENTITY_URI } from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class EntityService {

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  public constructor(private readonly http: HttpClient) { }

  public getById<T>(entityName: string, itemId: number): Observable<T> {
    return this.http.get<T>(`${API_FORM_ENTITY_URI}${entityName}/${itemId}`);
  }

  public get<T>(entityName: string): Observable<T[]> {
    return this.http.get<T[]>(`${API_FORM_ENTITY_URI}${entityName}`);
  }

  public add<T>(entityName: string, data: T): Observable<void> {
    this.http.post<T>(`${API_FORM_ENTITY_URI}${entityName}`, data, this.httpOptions).subscribe();

    return of();
  }

  public edit<T>(entityName: string, id: number, data: T): Observable<void> {
    this.http.put<T>(`${API_FORM_ENTITY_URI}${entityName}/${id}`, data, this.httpOptions).subscribe();

    return of();
  }
}
