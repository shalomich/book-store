import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationDataProvider {
  public get accessToken(): string {
    return localStorage.getItem('token') ?? '';
  }

  public get refreshToken(): string {
    return localStorage.getItem('refreshToken') ?? '';
  }

  public set accessToken(token: string) {
    localStorage.setItem('token', token);
  }

  public set refreshToken(token: string) {
    localStorage.setItem('refreshToken', token);
  }
}
