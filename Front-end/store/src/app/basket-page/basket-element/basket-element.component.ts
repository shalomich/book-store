import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

import { FormControl } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';

import { BasketProduct } from '../../core/interfaces/basket-product';
import { BasketService } from '../../core/services/basket.service';

@Component({
  selector: 'app-basket-element',
  templateUrl: './basket-element.component.html',
  styleUrls: ['./basket-element.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BasketElementComponent implements OnInit {

  @Input()
  public product: BasketProduct = {} as BasketProduct;

  public readonly quantity = new FormControl();

  constructor(public readonly basketService: BasketService, private readonly router: Router) {
  }

  ngOnInit(): void {
    this.quantity.setValue(this.product.quantity);
  }

  public increase(): void {
    this.quantity.setValue(<number> this.quantity.value + 1);
    this.basketService.increase(this.product);
  }

  public decrease(): void {
    this.quantity.setValue(<number> this.quantity.value - 1);
    this.basketService.decrease(this.product);
  }

  public handleProductClick(): void {
    window.open(`/book-store/catalog/book/${this.product.id}`, '_blank');
  }
}
