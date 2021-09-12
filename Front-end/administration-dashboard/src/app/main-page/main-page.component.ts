import { Component, OnInit } from '@angular/core';

import { ProductType } from '../core/interfaces/product-type';
import productTypeConfiguration from '../core/utils/product-type-configuration';

/** Main page's component. */
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {

  public readonly products: ProductType[] = productTypeConfiguration.getProducts();

  public constructor() {
  }

  public ngOnInit(): void {
  }

}
