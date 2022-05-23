import { Component, Input, OnInit } from '@angular/core';

import { ProductPreview } from '../../core/models/product-preview';
import { BasketService } from '../../core/services/basket.service';

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

  public inBasketFlag = false;

  public constructor(public readonly basketService: BasketService) { }

  public ngOnInit(): void {
    this.inBasketFlag = this.userBasketBookIds.includes(this.item.id);
  }

  public handleAddToBasketClick(): void {
    this.basketService.addProduct(this.item.id).subscribe(_ => {
      this.inBasketFlag = true;
    });
  }
}
