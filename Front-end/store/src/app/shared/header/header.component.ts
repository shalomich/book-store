import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Router } from '@angular/router';

import { map, switchMap, tap } from 'rxjs/operators';

import { AuthorizationDataProvider } from '../../core/services/authorization-data.provider';

import { AuthorizationService } from '../../core/services/authorization.service';

import { ProfileProviderService } from '../../core/services/profile-provider.service';

import { TokenValidationService } from '../../core/services/token-validation.service';

import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import {of} from 'rxjs';
import {UserProfile} from '../../core/models/user-profile';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class HeaderComponent implements OnInit {

  public user: UserProfile = new UserProfile();

  public isAuthorized = false;

  constructor(
    private dialog: MatDialog,
    private readonly authorizationService: AuthorizationService,
    private router: Router,
    private readonly profileService: ProfileProviderService,
    private readonly tokenValidationService: TokenValidationService,
  ) { }

  ngOnInit(): void {
    this.tokenValidationService.isTokenValid().pipe(
      switchMap(isValid => {
        if (isValid) {
          return this.profileService.getUserProfile();
        }

        return of({} as UserProfile);
      }),
    )
      .subscribe(profile => {
        this.isAuthorized = !!profile.id;
        this.user = profile;
    });
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }

  public logout(): void {
    this.authorizationService.logout();
  }
}
