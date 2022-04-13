import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { LOGIN_URL, REGISTER_URL } from '../utils/values';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {

  constructor(private readonly http: HttpClient) { }

  public login(email: string, password: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(LOGIN_URL, { email, password })
      .subscribe(data => {
        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);
        exitModalCallback();
        window.location.reload();
      },
        error => setErrorCallback());
  }

  public register(email: string, password: string, firstName: string, exitModalCallback: () => void, setErrorCallback: () => void): void {
    this.http.post<{accessToken: string; refreshToken: string;}>(REGISTER_URL, { email, password, firstName })
      .subscribe(data => {
        localStorage.setItem('token', data.accessToken);
        localStorage.setItem('refreshToken', data.refreshToken);
        exitModalCallback();
        window.location.reload();
      },
        error => setErrorCallback());
  }

  public logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    window.location.reload();
  }
}
