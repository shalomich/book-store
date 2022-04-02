import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { AuthorizationDataProvider } from '../../core/services/authorization-data.provider';

import { AuthorizationService } from '../../core/services/authorization.service';

import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { RegisterDialogComponent } from './register-dialog/register-dialog.component';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {

  constructor(
    private dialog: MatDialog,
    public authorizationDataProvider: AuthorizationDataProvider,
    private readonly authorizationService: AuthorizationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.authorizationDataProvider.token.asObservable().subscribe();
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }

  public logout(): void {
    this.authorizationService.logout();
    this.router.navigate(['/book-store']);
  }
}
