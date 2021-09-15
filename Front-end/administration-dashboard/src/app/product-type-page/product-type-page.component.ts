import { Component, OnInit } from '@angular/core';

import { ProductType } from '../core/interfaces/product-type';
import productTypeConfiguration from '../core/utils/product-type-configuration';

/** Main page's component. */
@Component({
  selector: 'app-product-type-page',
  templateUrl: './product-type-page.component.html',
  styleUrls: ['./product-type-page.component.css'],
})
export class ProductTypePageComponent implements OnInit {

  public readonly products: ProductType[] = productTypeConfiguration.getProducts();

  public constructor() {
  }

  public ngOnInit(): void {
  }

}
