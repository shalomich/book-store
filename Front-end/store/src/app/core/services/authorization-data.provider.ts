import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationDataProvider {
  public token: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public refreshToken: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
}
