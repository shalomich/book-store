import { Component, Input, OnInit } from '@angular/core';
import {SortingOptions} from '../../../core/interfaces/sorting-options';
import {BasketService} from '../../../core/services/basket.service';

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css'],
})
export class AddToCartComponent implements OnInit {

  @Input()
  public cost = '';

  @Input()
  public bookId: number = 0;

  public constructor(private readonly basketService: BasketService,) { }

  public ngOnInit(): void {
  }

  public handleBasketClick() {
    this.basketService.addProduct(this.bookId);
  }

}
