import { Component, OnInit } from '@angular/core';
import {OrderService} from '../core/services/order.service';
import {Observable} from 'rxjs';
import {Order} from '../core/models/order';

@Component({
  selector: 'app-orders-list-page',
  templateUrl: './orders-list-page.component.html',
  styleUrls: ['./orders-list-page.component.css']
})
export class OrdersListPageComponent implements OnInit {

  public orders$: Observable<Order[]> = new Observable<Order[]>();

  constructor(private readonly orderService: OrderService) {
    this.orders$ = this.orderService.getOrdersList();
  }

  ngOnInit(): void {
  }
}
