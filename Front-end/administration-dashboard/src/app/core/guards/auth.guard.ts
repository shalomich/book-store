import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { REFRESH_URL } from '../utils/values';
import {catchError, map} from 'rxjs/operators';
import {MatDialog} from '@angular/material/dialog';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private readonly jwtHelper: JwtHelperService,
    private readonly router: Router,
    private readonly http: HttpClient,
  ) {
  }


  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    const isRefreshSuccess = this.tryRefreshingTokens(token);
    if (!isRefreshSuccess) {
      this.router.navigate(['/book-store/catalog/book']);
    }
    return isRefreshSuccess;
  }

  private tryRefreshingTokens(token: string | null): Observable<boolean> {
    const refreshToken: string | null = localStorage.getItem('refreshToken');
    if (!token || !refreshToken) {
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
        localStorage.setItem('jwt', newToken);
        localStorage.setItem('refreshToken', newRefreshToken);
        return true;
      }),
      catchError(() => of(false)),
    );
  }

}
