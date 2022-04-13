import { Injectable } from '@angular/core';
import {Observable, of} from 'rxjs';
import {REFRESH_URL} from '../utils/values';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError, map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import {Router} from '@angular/router';
import {AuthorizationDataProvider} from './authorization-data.provider';

@Injectable({
  providedIn: 'root'
})
export class TokenValidationService {

  constructor(
    private readonly jwtHelper: JwtHelperService,
    private readonly router: Router,
    private readonly http: HttpClient,
    private readonly authorizationDataProvider: AuthorizationDataProvider,
  ) { }

  public isTokenValid(): Observable<boolean> {
    const token = this.authorizationDataProvider.accessToken;
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return of(true);
    }
    const isRefreshSuccess = this.tryRefreshingTokens(token);

    return isRefreshSuccess;
  }

  private tryRefreshingTokens(token: string | null): Observable<boolean> {
    const refreshToken: string | null = this.authorizationDataProvider.refreshToken;
    if (!token || !refreshToken) {
      this.router.navigate(['/book-store']);
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
        this.authorizationDataProvider.accessToken = newToken;
        this.authorizationDataProvider.refreshToken = newRefreshToken;
        return true;
      }),
      catchError(() => {
        this.router.navigate(['/book-store']);
        return of(false);
      }),
    );
  }
}
