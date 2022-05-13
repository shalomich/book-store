import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import {combineLatest, Observable, of, Subscription} from 'rxjs';

import { map, shareReplay, startWith, switchMap } from 'rxjs/operators';

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

  public newOrderId = 0;

  public isLastPage = false;

  private pageNumber = 1;

  constructor(private readonly orderService: OrderService) {
    this.orders$ = this.orderService.getOrdersList(this.pageNumber);
  }

  ngOnInit(): void {
    if (sessionStorage.getItem('createdOrderId')) {
      this.newOrderId = Number(sessionStorage.getItem('createdOrderId'));
      sessionStorage.removeItem('createdOrderId');
    }
  }

  public getOrderTotalCost(order: Order): number {
    return getTotalCost(order.products);
  }

  public showMoreOrders(): void {
    this.pageNumber += 1;

    this.orders$ = combineLatest([this.orders$, this.orderService.getOrdersList(this.pageNumber)]).pipe(
      map(([currentOrders, loadedOrders]) => {
        if (!loadedOrders.length) {
          this.isLastPage = true;
          return currentOrders;
        }

        return currentOrders.concat(loadedOrders);
      }),
    );
  }
}
