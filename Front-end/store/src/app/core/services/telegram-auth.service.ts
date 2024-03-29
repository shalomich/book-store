import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { Route, Router } from '@angular/router';

import { PROFILE_URL, TELEGRAM_URL } from '../utils/values';

import { UserProfile } from '../models/user-profile';

import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class TelegramAuthService {

  constructor(private readonly http: HttpClient, private readonly authorizationService: AuthorizationService) { }

  public getTelegramToken(phoneNumber: string, user: UserProfile): Observable<{ botToken: string; }> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return of(null).pipe(
      switchMap(_ => {
        if (phoneNumber !== user.phoneNumber) {
          user.phoneNumber = phoneNumber;
          return this.http.put<void>(PROFILE_URL, user, { headers });
        }

        return of(null);
      }),
      switchMap(_ => this.http.post<{ botToken: string; }>(`${TELEGRAM_URL}/token`, {}, { headers })),
    );
  }

  public redirectToTelegram(): void {
    window.open(`https://t.me/ComicsStore_bot`, '_blank');
  }

  public redirectToTelegramWithAuth(token: string): void {
    window.open(`https://t.me/Comic_Store_Bot?start=${token}`, '_self');
  }

  public unlinkTelegramBot(): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.delete<void>(TELEGRAM_URL, { headers });
  }
}
