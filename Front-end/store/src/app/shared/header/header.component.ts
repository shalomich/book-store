import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Router } from '@angular/router';

import { AuthorizationDataProvider } from '../../core/services/authorization-data.provider';

import { AuthorizationService } from '../../core/services/authorization.service';

import { ProfileService } from '../../core/services/profile.service';

import { LoginDialogComponent } from './login-dialog/login-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class HeaderComponent implements OnInit {

  public userName = '';

  public isAuthorized = false;

  constructor(
    private dialog: MatDialog,
    private readonly authorizationService: AuthorizationService,
    private router: Router,
    private readonly profileService: ProfileService,
  ) { }

  ngOnInit(): void {
    this.profileService.userProfile.subscribe(profile => {
      this.userName = profile.firstName;
      this.isAuthorized = this.profileService.isUserAuthorized;
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
