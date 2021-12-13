import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginPageComponent implements OnInit {
  public email: FormControl = new FormControl();

  public password: FormControl = new FormControl();

  constructor(private dialog: MatDialog,
    private readonly authService: AuthorizationService,
    private readonly authProvider: AuthorizationDataProvider) { }

  ngOnInit(): void {
  }

  public login() {
    this.authService.login(this.email.value, this.password.value);
    this.dialog.closeAll();
  }

}
