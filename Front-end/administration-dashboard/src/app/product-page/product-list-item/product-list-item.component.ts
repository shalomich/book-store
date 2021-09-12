import { Component, Input, OnInit } from '@angular/core';

import { ProductPreview } from '../../core/models/product-preview';

@Component({
  selector: 'app-entity-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrls: ['./product-list-item.component.css'],
})
export class ProductListItemComponent implements OnInit {

  @Input()
  public item: ProductPreview = {} as ProductPreview;

  public constructor() { }

  public ngOnInit(): void {
  }

}