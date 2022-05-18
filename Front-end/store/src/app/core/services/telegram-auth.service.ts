import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { PROFILE_URL, TELEGRAM_AUTH_URL } from '../utils/values';
import {AuthorizationService} from './authorization.service';
import {Route, Router} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class TelegramAuthService {

  constructor(private readonly http: HttpClient, private readonly authorizationService: AuthorizationService) { }

  public getTelegramToken(phoneNumber: string): Observable<any> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const body = {
      phoneNumber,
    };

    return this.http.post<any>(TELEGRAM_AUTH_URL, {}, { headers });
  }

  public redirectToTelegram(token: string): void {
    window.open(`https://t.me/Comic_Store_Bot?start=${token}`, '_self');
  }
}
