import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable } from 'rxjs';

import { OrderService } from '../core/services/order.service';
import { Order } from '../core/models/order';
import { getTotalCost } from '../core/utils/helpers';

@Component({
  selector: 'app-orders-list-page',
  templateUrl: './orders-list-page.component.html',
  styleUrls: ['./orders-list-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class OrdersListPageComponent implements OnInit {

  public orders$: Observable<Order[]> = new Observable<Order[]>();

  constructor(private readonly orderService: OrderService) {
    this.orders$ = this.orderService.getOrdersList();
  }

  ngOnInit(): void {
  }

  public getOrderTotalCost(order: Order): number {
    return getTotalCost(order.products);
  }
}
