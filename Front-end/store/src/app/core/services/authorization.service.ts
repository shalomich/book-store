import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { LOGIN_URL, REGISTER_URL } from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {

  constructor(private readonly http: HttpClient) { }

  public get accessToken(): string | null {
    return localStorage.getItem('token');
  }

  public get refreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  public set accessToken(token: string | null) {
    if (token) {
      localStorage.setItem('token', token);
    } else {
      localStorage.removeItem('token');
    }
  }

  public set refreshToken(token: string | null) {
    if (token) {
      localStorage.setItem('refreshToken', token);
    } else {
      localStorage.removeItem('token');
    }
  }

  public login(email: string, password: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(LOGIN_URL, { email, password })
      .subscribe(data => {
        this.accessToken = data.accessToken;
        this.refreshToken = data.refreshToken;
        exitModalCallback();
        window.location.reload();
      },
        error => setErrorCallback());
  }

  public register(email: string, password: string, firstName: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(REGISTER_URL, { email, password, firstName })
      .subscribe(data => {
        this.accessToken = data.accessToken;
        this.refreshToken = data.refreshToken;
        exitModalCallback();
        window.location.reload();
      },
        error => setErrorCallback());
  }

  public logout(): void {
    this.accessToken = null;
    this.refreshToken = null;
    window.location.reload();
  }
}
