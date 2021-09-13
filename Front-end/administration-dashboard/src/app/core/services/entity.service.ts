import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { API_FORM_ENTITY_URI } from '../utils/values';

@Injectable()
export abstract class EntityService {

  protected constructor(private readonly http: HttpClient) { }

  protected getEntityPage<T>(entityName: string): Observable<T[]> {
    return this.http.get<T[]>(`${API_FORM_ENTITY_URI}${entityName}`);
  }
}
