import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { LOGIN_URL, REGISTER_URL } from '../utils/values';

import { AuthorizationDataProvider } from './authorization-data.provider';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {

  constructor(private readonly http: HttpClient, private readonly authProvider: AuthorizationDataProvider) { }

  public login(email: string, password: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(LOGIN_URL, { email, password })
      .subscribe(data => {
        this.authProvider.token.next(data.accessToken);
        this.authProvider.refreshToken.next(data.refreshToken);
        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);
        exitModalCallback();
      },
        error => setErrorCallback());
  }

  public register(email: string, password: string, firstName: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(REGISTER_URL, { email, password, firstName })
      .subscribe(data => {
        this.authProvider.token.next(data.accessToken);
        this.authProvider.refreshToken.next(data.refreshToken);
        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);
        exitModalCallback();
      },
        error => setErrorCallback());
  }

  public logout(): void {
    this.authProvider.token.next(null);
    this.authProvider.refreshToken.next(null);
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }
}
