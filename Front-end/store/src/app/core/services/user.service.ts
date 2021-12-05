import { Injectable } from '@angular/core';
import {User} from '../interfaces/user';
import {HttpClient} from '@angular/common/http';
import {LOGIN_URL, REGISTER_URL} from '../utils/values';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public currentUser: Observable<User> = new Observable<User>();

  constructor(private readonly http: HttpClient) { }

  public loginUser(email: string, password: string): void {
    this.currentUser = this.http.post<User>(LOGIN_URL, { email, password });
  }

  public registerUser(email: string, password: string): void {
    this.currentUser = this.http.post<User>(REGISTER_URL, { email, password });
  }
}
