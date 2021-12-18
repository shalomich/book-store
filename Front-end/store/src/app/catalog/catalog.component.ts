import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs';

import { ProductPreview } from '../core/models/product-preview';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CatalogComponent implements OnInit {

  public readonly propertyNamesWithText: Array<[string, string]> = [['По имени', 'name'], ['По цене', 'cost'], ['По дате добавления', 'addingDate']];

  @Input() public books$: Observable<ProductPreview[]> = new Observable<ProductPreview[]>();

  @Input() paramsBuilder!: ProductParamsBuilderService;

  @Input() disableFilters: Array<string> = [];

  constructor() { }

  ngOnInit(): void {
  }

}