import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AuthorizationService } from '../core/services/authorization.service';
import { AuthorizationDataProvider } from '../core/services/authorization-data.provider';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginPageComponent implements OnInit {
  public email: FormControl = new FormControl();

  public password: FormControl = new FormControl();

  constructor(
    private readonly authService: AuthorizationService,
    private readonly authProvider: AuthorizationDataProvider,
    private readonly router: Router,
  ) { }

  ngOnInit(): void {
  }

  public login() {
    this.authService.login(this.email.value, this.password.value);
    this.router.navigate(['/dashboard/product']);
  }
}
