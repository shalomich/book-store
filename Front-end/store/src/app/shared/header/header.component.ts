import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { ActivatedRoute, Router } from '@angular/router';

import { map, switchMap, tap } from 'rxjs/operators';

import { of, Subscription } from 'rxjs';

import { AuthorizationService } from '../../core/services/authorization.service';

import { ProfileProviderService } from '../../core/services/profile-provider.service';

import { TokenValidationService } from '../../core/services/token-validation.service';

import { UserProfile } from '../../core/models/user-profile';

import { LoginDialogComponent } from './login-dialog/login-dialog.component';

import { TelegramAuthDialogComponent } from './telegram-auth-dialog/telegram-auth-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class HeaderComponent implements OnInit, OnDestroy {

  public user: UserProfile = new UserProfile();

  public isAuthorized = false;

  private subs: Subscription = new Subscription();

  constructor(
    private dialog: MatDialog,
    private readonly authorizationService: AuthorizationService,
    private router: Router,
    private readonly activatedRoute: ActivatedRoute,
    private readonly profileService: ProfileProviderService,
    private readonly tokenValidationService: TokenValidationService,
  ) { }

  public ngOnInit(): void {
    this.subs.add(this.tokenValidationService.isTokenValid(false).pipe(
      switchMap(isValid => {
        if (isValid) {
          return this.profileService.getUserProfile();
        }

        return of({} as UserProfile);
      }),
      switchMap(profile => {
        this.isAuthorized = !!profile.id;
        this.user = profile;

        return this.activatedRoute.queryParams;
      }),
    )
      .subscribe(params => {
        if (params.openTelegramBotDialog) {
          this.openTelegramDialog();
        }
    }));
  }

  public ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }

  public logout(): void {
    this.authorizationService.logout();
  }

  public openTelegramDialog(): void {
    if (this.isAuthorized) {
      this.dialog.open(TelegramAuthDialogComponent, {
        data: { user: this.user },
        autoFocus: false,
        panelClass: 'telegram-dialog',
      });
    } else {
      this.openLoginDialog();
    }
  }
}
