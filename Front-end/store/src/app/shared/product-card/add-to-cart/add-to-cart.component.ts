import { Component, Input, OnInit } from '@angular/core';

import { SortingOptions } from '../../../core/interfaces/sorting-options';
import { BasketService } from '../../../core/services/basket.service';
import {LoginDialogComponent} from '../../header/login-dialog/login-dialog.component';
import {MatDialog} from '@angular/material/dialog';

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

  @Input()
  public isUserAuthorized = false;

  public constructor(private readonly basketService: BasketService, private readonly dialog: MatDialog) { }

  public ngOnInit(): void {
  }

  public handleAddToBasketClick() {
    this.basketService.addProduct(this.bookId).subscribe(_ => {
      this.isInBasket = true;
    });
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }
}
