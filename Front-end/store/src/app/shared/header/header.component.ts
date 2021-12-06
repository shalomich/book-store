import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import {RegisterDialogComponent} from './register-dialog/register-dialog.component';
import {AuthorizationDataProvider} from '../../core/services/authorization-data.provider';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {

  constructor(private dialog: MatDialog, private readonly authorizationDataProvider: AuthorizationDataProvider) { }

  ngOnInit(): void {
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }

  public checkAuthorization(): boolean {
    return this.authorizationDataProvider.isAuthorized();
  }
}
