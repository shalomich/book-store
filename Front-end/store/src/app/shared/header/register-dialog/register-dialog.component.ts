import { Component, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { FormControl } from '@angular/forms';

import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { UserService } from '../../../core/services/user.service';
import {RegisterValidator} from '../../../core/validators/register-validator';

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css'],
})
export class RegisterDialogComponent implements OnInit {

  public email: FormControl = new FormControl();

  public password: FormControl = new FormControl();

  public repeatPassword: FormControl = new FormControl();

  constructor(private dialog: MatDialog, private readonly userService: UserService) {
    this.repeatPassword.setValidators(RegisterValidator.matchPassword(this.password));
  }

  ngOnInit(): void {
  }

  public openLoginDialog(e: Event) {
    e.preventDefault();
    this.dialog.closeAll();
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  public register(): void {
    this.userService.registerUser(this.email.value, this.password.value);
    this.dialog.closeAll();
  }
}
