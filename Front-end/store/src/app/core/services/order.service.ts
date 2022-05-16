import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { UserProfile } from '../models/user-profile';

import { ORDER_URL } from '../utils/values';

import { Order } from '../models/order';
import { OrderDto } from '../DTOs/order-dto';
import { OrderMapper } from '../mappers/mapper/order.mapper';

import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

  constructor(
    private readonly http: HttpClient,
    private readonly authorizationService: AuthorizationService,
    private readonly orderMapper: OrderMapper,
  ) { }

  public applyOrder(personalData: UserProfile): Observable<number> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const body = {
      firstName: personalData.firstName,
      lastName: personalData.lastName,
      phoneNumber: personalData.phoneNumber,
      email: personalData.email,
      orderReceiptMethod: 'Pickup',
      paymentMethod: 'Offline',
    };

    return this.http.post<number>(ORDER_URL, body, { headers });
  }

  public getOrdersList(pageNumber: number): Observable<Order[]> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.get<OrderDto[]>(`${ORDER_URL}/?pageSize=4&&pageNumber=${pageNumber}`, { headers }).pipe(
      map(orders => orders.map(order => this.orderMapper.fromDto(order))),
    );
  }

  public cancelOrder(orderId: number): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.put<void>(`${ORDER_URL}/${orderId}/cancelled`, orderId, { headers });
  }
}
