import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TagsGroup } from '../interfaces/tags-group';
import { TAGS_URL } from '../utils/values';

import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class TagsService {

  constructor(private readonly http: HttpClient, private readonly authorizationService: AuthorizationService) { }

  public getTagsGroups(): Observable<TagsGroup[]> {
    return this.http.get<TagsGroup[]>(`${TAGS_URL}/group`);
  }

  public updateUsersTags(tagsIds: number[]): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };


    return this.http.put<void>(`${TAGS_URL}`, { tagIds: tagsIds }, { headers });
  }
}
