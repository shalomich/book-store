import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { catchError, map } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';

import { REFRESH_URL } from '../utils/values';
import { TokenValidationService } from '../services/token-validation.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private readonly jwtHelper: JwtHelperService,
    private readonly router: Router,
    private readonly tokenValidationService: TokenValidationService,
  ) {
  }


  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.tokenValidationService.isTokenValid(true);
  }
}
