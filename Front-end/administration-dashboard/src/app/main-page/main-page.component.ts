import { Component, OnInit } from '@angular/core';

import ProductsConfig from '../../products-config.json';

import { ProductType } from '../core/interfaces/product-type';

/** Main page's component. */
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {

  public readonly products = ProductsConfig as ProductType[];

  public constructor() {
  }

  public ngOnInit(): void {
  }

}
