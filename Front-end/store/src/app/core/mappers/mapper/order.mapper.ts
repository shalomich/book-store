import { Injectable } from '@angular/core';

import { OrderDto } from '../../DTOs/order-dto';
import { Order } from '../../models/order';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class OrderMapper {
  /** @inheritdoc */
  public fromDto(data: OrderDto): Order {
    return new Order({
      id: data.id,
      state: data.state,
      userName: data.userName,
      email: data.email,
      phoneNumber: data.phoneNumber,
      address: data.orderReceiptMethod,
      orderReceiptMethod: data.orderReceiptMethod,
      paymentMethod: data.paymentMethod,
      placedDate: data.placedDate,
      deliveredDate: data.deliveredDate,
      products: data.products,
    });
  }
}
