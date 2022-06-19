import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, combineLatest, Observable, of, Subject, Subscription } from 'rxjs';

import { map, scan, shareReplay, startWith, switchMap } from 'rxjs/operators';

import { MatDialog } from '@angular/material/dialog';

import { OrderService } from '../core/services/order.service';
import { Order } from '../core/models/order';
import { getTotalCost } from '../core/utils/helpers';
import { ConfirmationModalComponent } from '../shared/confirmation-modal/confirmation-modal.component';
import { ORDER_CANCEL_MESSAGE } from '../core/utils/messages';

@Component({
  selector: 'app-orders-list-page',
  templateUrl: './orders-list-page.component.html',
  styleUrls: ['./orders-list-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class OrdersListPageComponent implements OnInit, OnDestroy {

  public orders: Order[] = [];

  public newOrderId = 0;

  public isLastPage = false;

  public loading = true;

  public pageNumber$: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  private subs: Subscription = new Subscription();

  constructor(private readonly orderService: OrderService, private readonly dialog: MatDialog) {
  }

  ngOnInit(): void {
    if (sessionStorage.getItem('createdOrderId')) {
      this.newOrderId = Number(sessionStorage.getItem('createdOrderId'));
      sessionStorage.removeItem('createdOrderId');
    }

    this.subs.add(this.pageNumber$.asObservable().pipe(
      switchMap(pageNumber => this.orderService.getOrdersList(pageNumber)),
      map(loadedOrders => {
        if (!loadedOrders.length) {
          this.isLastPage = true;
        }
        this.orders = this.orders.concat(loadedOrders);

        this.loading = false;
      }),
    )
      .subscribe());

    this.pageNumber$.next(1);
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public getOrderTotalCost(order: Order): number {
    return getTotalCost(order.products);
  }

  public showMoreOrders(): void {
    this.pageNumber$.next(this.pageNumber$.value + 1);
  }

  public onCancelOrder(orderId: number): void {
    this.dialog.open(ConfirmationModalComponent, {
      data: { onConfirm: this.cancelOrder(orderId), message: ORDER_CANCEL_MESSAGE },
      autoFocus: false,
      restoreFocus: false,
    });
  }

  private cancelOrder(orderId: number): () => {} {
    return () => this.subs.add(this.orderService.cancelOrder(orderId).subscribe(_ => window.location.reload()));
  }
}
