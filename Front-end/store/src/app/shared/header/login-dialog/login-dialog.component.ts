import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {RegisterDialogComponent} from '../register-dialog/register-dialog.component';
import {UserService} from '../../../core/services/user.service';
import {FormControl} from '@angular/forms';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginDialogComponent implements OnInit {

  public email: FormControl = new FormControl();

  public password: FormControl = new FormControl();

  constructor(private dialog: MatDialog, private readonly userService: UserService) { }

  ngOnInit(): void {
  }

  public openRegisterDialog(e: Event) {
    e.preventDefault();
    this.dialog.closeAll();
    const dialogRef = this.dialog.open(RegisterDialogComponent, {
      width: 'min-content',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  public login() {
    this.userService.loginUser(this.email.value, this.password.value);
    this.dialog.closeAll();
  }
}
