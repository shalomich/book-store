import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { TagsGroup } from '../interfaces/tags-group';

@Injectable({
  providedIn: 'root',
})
export class TagsService {

  constructor(private readonly http: HttpClient) { }

  public getTagsGroups(): Observable<TagsGroup[]> {
    return of([
      {
        id: 1,
        name: 'Вселенные',
        tags: [
          {
            id: 1,
            name: 'DC',
          },
          {
            id: 2,
            name: 'Marvel',
          },
        ],
      },
      {
        id: 2,
        name: 'Персонажи',
        tags: [
          {
            id: 3,
            name: 'Бэтмен',
          },
          {
            id: 4,
            name: 'Зеленый фонарь',
          },
        ],
      },
      {
        id: 3,
        name: 'Разное',
        tags: [
          {
            id: 5,
            name: 'Школа',
          },
          {
            id: 6,
            name: 'Мертвецы',
          },
          {
            id: 7,
            name: 'Космос',
          },
        ],
      },
    ]);
  }
}
