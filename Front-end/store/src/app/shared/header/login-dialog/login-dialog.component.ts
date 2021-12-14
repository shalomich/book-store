import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {RegisterDialogComponent} from '../register-dialog/register-dialog.component';
import {AuthorizationService} from '../../../core/services/authorization.service';
import {FormControl} from '@angular/forms';
import {AuthorizationDataProvider} from "../../../core/services/authorization-data.provider";

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginDialogComponent implements OnInit {

  public email: FormControl = new FormControl();

  public password: FormControl = new FormControl();

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
    });
  }

  public login() {
    this.authService.login(this.email.value, this.password.value);
    this.dialog.closeAll();
  }
}
