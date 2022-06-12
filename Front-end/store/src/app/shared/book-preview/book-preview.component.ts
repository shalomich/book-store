import { Component, Input, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { ProductPreview } from '../../core/models/product-preview';
import { BasketService } from '../../core/services/basket.service';
import { LoginDialogComponent } from '../header/login-dialog/login-dialog.component';

@Component({
  selector: 'app-book-preview',
  templateUrl: './book-preview.component.html',
  styleUrls: ['./book-preview.component.css'],
})
export class BookPreviewComponent implements OnInit {

  @Input()
  public item: ProductPreview = {} as ProductPreview;

  @Input()
  public userBasketBookIds: number[] = [];

  @Input()
  public isUserAuthorized = false;

  public inBasketFlag = false;

  public constructor(public readonly basketService: BasketService, private readonly dialog: MatDialog) { }

  public ngOnInit(): void {
    this.inBasketFlag = this.userBasketBookIds.includes(this.item.id);
  }

  public handleAddToBasketClick(): void {
    this.basketService.addProduct(this.item.id).subscribe(_ => {
      this.inBasketFlag = true;
    });
  }

  public openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: 'min-content',
    });
  }
}
