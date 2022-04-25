import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { UserProfile } from '../models/user-profile';

import { ORDER_URL } from '../utils/values';

import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

  constructor(private readonly http: HttpClient, private readonly authorizationService: AuthorizationService) { }

  public applyOrder(personalData: UserProfile): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const body = {
      firstName: personalData.firstName,
      lastName: personalData.lastName,
      phoneNumber: personalData.phoneNumber,
      email: personalData.email,
      address: '',
      orderReceiptMethod: 1,
      paymentMethod: 1,
    };

    return this.http.post<void>(ORDER_URL, body, { headers });
  }
}
