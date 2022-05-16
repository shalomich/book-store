import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

import { REFRESH_URL } from '../utils/values';

import { AuthorizationDataProvider } from './authorization-data.provider';
import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class TokenValidationService {

  constructor(
    private readonly jwtHelper: JwtHelperService,
    private readonly router: Router,
    private readonly http: HttpClient,
    private readonly authorizationService: AuthorizationService,
  ) { }

  public isTokenValid(redirectNeeded: boolean): Observable<boolean> {
    const token = this.authorizationService.accessToken;
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return of(true);
    }
    const isRefreshSuccess = this.tryRefreshingTokens(token, redirectNeeded);

    return isRefreshSuccess;
  }

  private tryRefreshingTokens(token: string | null, redirectNeeded: boolean): Observable<boolean> {
    const { refreshToken } = this.authorizationService;
    if (!token || !refreshToken) {
      if (redirectNeeded) {
        this.router.navigate(['/book-store']);
      }
      return of(false);
    }
    const credentials = JSON.stringify({ accessToken: token, refreshToken });
    return this.http.post(REFRESH_URL, credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      observe: 'response',
    }).pipe(
      map(response => {
        const newToken = (<any>response).body.accessToken;
        const newRefreshToken = (<any>response).body.refreshToken;
        this.authorizationService.accessToken = newToken;
        this.authorizationService.refreshToken = newRefreshToken;
        return true;
      }),
      catchError(() => {
        this.router.navigate(['/book-store']);
        return of(false);
      }),
    );
  }
}
