import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';

import ProductsConfig from '../../products-config.json';

import { Products } from '../core/interfaces/products';

/** Main page's component. */
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {

  public readonly products$: Observable<Products[]>;

  public constructor() {
    this.products$ = of(ProductsConfig as Products[]);
  }

  public ngOnInit(): void {
  }

}
