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

  public constructor(public readonly basketService: BasketService) { }

  public ngOnInit(): void {
  }
}
