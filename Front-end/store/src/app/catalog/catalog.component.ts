import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { map } from 'rxjs/operators';

import { PaginationInstance } from 'ngx-pagination';

import { ProductPreview } from '../core/models/product-preview';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreviewSet } from '../core/models/product-preview-set';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { PAGE_SIZE } from '../core/utils/values';


@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CatalogComponent implements OnInit {

  public readonly propertyNamesWithText: Array<[string, string]> = [['По имени', 'name'], ['По цене', 'cost'], ['По дате добавления', 'addingDate']];

  @Input() bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  @Input() paramsBuilder!: ProductParamsBuilderService;

  @Input() disableFilters: Array<string> = [];

  public books: ProductPreview[] = [];

  constructor() {
  }

  public config: PaginationInstance = {
    id: 'paginationPanel',
    currentPage: 0,
    itemsPerPage: 0,
    totalItems: 0,
  };

  public onPageChanged(number: number): void {

    if (number === this.paramsBuilder.paginationOptions$.value.pageNumber) {
      return;
    }

    this.config.currentPage = number;

    this.paramsBuilder.paginationOptions$.next({
      pageSize: PAGE_SIZE,
      pageNumber: number,
    });
  }

  ngOnInit(): void {
    this.bookSet$.subscribe(data => {
      const { pageNumber, pageSize } = this.paramsBuilder.paginationOptions$.value;

      this.books = data.previews;
      this.config = {
        ...this.config,
        currentPage: pageNumber,
        itemsPerPage: pageSize,
        totalItems: data.totalCount,
      };
    });
  }

}
