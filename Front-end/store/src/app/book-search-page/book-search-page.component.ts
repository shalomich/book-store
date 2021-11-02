import { Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, Observable } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { PaginationControlsComponent, PaginationInstance } from 'ngx-pagination';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_NUMBER, PAGE_SIZE } from '../core/utils/values';

@AutoUnsubscribe()
@Component({
  selector: 'app-book-search-page',
  templateUrl: './book-search-page.component.html',
  styleUrls: ['./book-search-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BookSearchPageComponent implements OnInit, OnDestroy {

  public readonly books$: Observable<ProductPreview[]>;

  public config: PaginationInstance = {
    id: 'paginationPanel',
    itemsPerPage: PAGE_SIZE,
    currentPage: PAGE_NUMBER,
    totalItems: 0,
  };

  public readonly paginationOptions$: BehaviorSubject<PaginationOptions> = new BehaviorSubject<PaginationOptions>({
    pageSize: PAGE_SIZE,
    pageNumber: PAGE_NUMBER,
  });

  public constructor(
    private readonly bookService: BookService,
    private readonly productParamsBuilderService: ProductParamsBuilderService,
  ) {
    this.books$ = this.paginationOptions$.asObservable().pipe(
      switchMap(options => {
        this.productParamsBuilderService.setPaging(options);
        this.config.currentPage = options.pageNumber;
        return this.bookService.get(this.productParamsBuilderService.params);
      }),
    );
  }

  public ngOnInit(): void {
    this.paginationOptions$.asObservable().pipe(
      switchMap(options => {
        this.productParamsBuilderService.setPaging(options);
        return this.bookService.getQuantity(this.productParamsBuilderService.params);
      }),

    ).subscribe(quantity => {
      this.config.totalItems = quantity;
    });
  }

  public ngOnDestroy() {
  }

  public onPageChanged(number: number): void {
    this.paginationOptions$.next({
      pageSize: PAGE_SIZE,
      pageNumber: number,
    });
  }

}