import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TagsGroup } from '../interfaces/tags-group';
import {TAGS_URL} from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class TagsService {

  constructor(private readonly http: HttpClient) { }

  public getTagsGroups(): Observable<TagsGroup[]> {
    return this.http.get<TagsGroup[]>(`${TAGS_URL}/group`);
  }
}
