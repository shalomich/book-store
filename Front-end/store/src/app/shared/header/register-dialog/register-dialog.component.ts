import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { AuthorizationService } from '../../../core/services/authorization.service';
import { AuthValidator } from '../../../core/validators/auth-validator';
import {EXISTING_EMAIL_ERROR} from '../../../core/utils/validation-errors';

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class RegisterDialogComponent implements OnInit {

  public registerForm = new FormGroup({
    email: new FormControl('', { validators: [AuthValidator.emailFormat(), Validators.required], updateOn: 'blur' }),
    name: new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    password: new FormControl('', { validators: [Validators.required, AuthValidator.passwordFormat()], updateOn: 'blur' }),
    repeatPassword: new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
  });

  constructor(private dialog: MatDialog, private readonly authService: AuthorizationService) {}

  ngOnInit(): void {
    this.registerForm.controls.repeatPassword.setValidators(AuthValidator.matchPassword(this.registerForm.controls.password));
  }

  public openLoginDialog(e: Event) {
    e.preventDefault();
    this.dialog.closeAll();
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }

  public register(): void {
    this.authService.register(this.registerForm.value.email, this.registerForm.value.password, this.registerForm.value.name,
      () => this.close(), () => this.setExistingEmailError());
  }

  public close() {
    this.dialog.closeAll();
  }

  public setExistingEmailError() {
    this.registerForm.controls.email.setErrors({ error: EXISTING_EMAIL_ERROR });
  }
}
