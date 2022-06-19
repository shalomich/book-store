import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { RegisterDialogComponent } from '../register-dialog/register-dialog.component';
import { AuthorizationService } from '../../../core/services/authorization.service';
import { AuthorizationDataProvider } from '../../../core/services/authorization-data.provider';
import {LOGIN_ERROR} from '../../../core/utils/validation-errors';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginDialogComponent implements OnInit {
  public loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  public hidePassword = true;

  constructor(private dialog: MatDialog,
    private readonly authService: AuthorizationService,
    private readonly authProvider: AuthorizationDataProvider) { }

  ngOnInit(): void {
  }

  public openRegisterDialog(e: Event) {
    e.preventDefault();
    this.dialog.closeAll();
    const dialogRef = this.dialog.open(RegisterDialogComponent, {
      width: 'min-content',
      panelClass: 'auth-dialog',
    });
  }

  public login() {
    this.authService.login(this.loginForm.controls.email.value, this.loginForm.controls.password.value,
      () => this.close(), () => this.setLoginError());
  }

  public close() {
    this.dialog.closeAll();
  }

  public setLoginError() {
    this.loginForm.setErrors({ error: LOGIN_ERROR });
  }
}
