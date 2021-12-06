import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LOGIN_URL, REGISTER_URL} from '../utils/values';
import {Observable} from 'rxjs';
import {AuthorizationDataProvider} from "./authorization-data.provider";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(private readonly http: HttpClient, private readonly authProvider: AuthorizationDataProvider) { }

  public login(email: string, password: string): void {
    this.http.post<string>(LOGIN_URL, { email, password })
      .subscribe(token => this.authProvider.token = token);
  }

  public register(email: string, password: string): void {
    this.http.post<string>(REGISTER_URL, { email, password })
      .subscribe(token => this.authProvider.token = token);
  }

  public logout(): void {
    this.authProvider.token = undefined;
  }
}
