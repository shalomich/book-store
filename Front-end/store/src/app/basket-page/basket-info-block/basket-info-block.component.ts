import { Component, Input, OnInit } from '@angular/core';

import { Observable } from 'rxjs';

import { BasketProduct } from '../../core/models/basket-product';

@Component({
  selector: 'app-basket-info-block',
  templateUrl: './basket-info-block.component.html',
  styleUrls: ['./basket-info-block.component.css'],
})
export class BasketInfoBlockComponent implements OnInit {

  @Input()
  public products: Observable<BasketProduct[]> = new Observable<BasketProduct[]>();

  public totalCost = 0;

  public totalAmount = 0;

  constructor() {
  }

  public ngOnInit(): void {
    this.products.subscribe(products => {
      this.totalAmount = products.reduce((sum, a) => sum + a.quantity, 0);

      this.totalCost = products.reduce((sum, a) => sum + (a.cost * a.quantity), 0);
    });
  }

}
