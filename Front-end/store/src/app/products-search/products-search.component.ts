import { Component, OnInit, ViewChild } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';

@Component({
  selector: 'app-products-search',
  templateUrl: './products-search.component.html',
  styleUrls: ['./products-search.component.css'],
})
export class ProductsSearchComponent implements OnInit {

  public readonly books$: Observable<ProductPreview[]>;

  public readonly maxPagesNumber = 7;

  public readonly paginationOptions$: BehaviorSubject<PaginationOptions> = new BehaviorSubject<PaginationOptions>({
    pageSize: 2,
    pageNumber: 1,
  });

  public constructor(
    private readonly bookService: BookService,
    private readonly productParamsBuilderService: ProductParamsBuilderService,
  ) {
    this.books$ = this.paginationOptions$.asObservable().pipe(
      switchMap(options => {
        this.productParamsBuilderService.setPaging(options);
         return this.bookService.get(this.productParamsBuilderService.params);
      }),
    );
  }

  public ngOnInit(): void {
  }

  public onPageChanged(number: number): void {
    console.log(number);
  }

}
