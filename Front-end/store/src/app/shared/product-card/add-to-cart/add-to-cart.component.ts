import { Component, Input, OnInit } from '@angular/core';

import { SortingOptions } from '../../../core/interfaces/sorting-options';
import { BasketService } from '../../../core/services/basket.service';

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css'],
})
export class AddToCartComponent implements OnInit {

  @Input()
  public cost = '';

  @Input()
  public bookId = 0;

  @Input()
  public isInBasket = false;

  public inBasketFlag = false;

  public constructor(private readonly basketService: BasketService) { }

  public ngOnInit(): void {
    this.inBasketFlag = this.isInBasket;
  }

  public handleAddToBasketClick() {
    this.basketService.addProduct(this.bookId).subscribe(_ => {
      this.inBasketFlag = true;
    });
  }
}
