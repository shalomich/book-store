import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';

import { map, switchMap } from 'rxjs/operators';

import { BookBattle } from '../models/book-battle';
import { BookBattleMapper } from '../mappers/book-battle-mapper';
import { BookBattleDto } from '../DTOs/book-battle-dto';
import { BATTLE_URL } from '../utils/values';


import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class BattleService {

  constructor(
    private readonly http: HttpClient,
    private readonly bookBattleMapper: BookBattleMapper,
    private readonly authorizationService: AuthorizationService,
  ) { }

  public getBattleInfo(): Observable<BookBattle> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.get<BookBattleDto>(BATTLE_URL, { headers }).pipe(
      map(battle => this.bookBattleMapper.fromDto(battle)),
    );
  }

  public vote(firstTimeVoting: boolean, bookId: number, points: number): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    if (firstTimeVoting) {
      return this.http.post<void>(`${BATTLE_URL}/vote`, bookId, { headers }).pipe(
        switchMap(_ => this.http.put<void>(`${BATTLE_URL}/vote`, points, { headers })),
      );
    }

    return this.http.put<void>(`${BATTLE_URL}/vote`, points, { headers });
  }
}
