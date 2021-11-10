import { Component, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { PaginationInstance } from 'ngx-pagination';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_NUMBER, PAGE_SIZE } from '../core/utils/values';
import { RelatedEntity } from '../core/models/related-entity';

@AutoUnsubscribe()
@Component({
  selector: 'app-book-search-page',
  templateUrl: './book-search-page.component.html',
  styleUrls: ['./book-search-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BookSearchPageComponent implements OnInit, OnDestroy {

  public books$: Observable<ProductPreview[]>;

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

  public readonly genres$: Observable<RelatedEntity[]>;

  public readonly authors$: Observable<RelatedEntity[]>;

  public readonly bookTypes$: Observable<RelatedEntity[]>;

  public readonly publishers$: Observable<RelatedEntity[]>;

  public readonly coverArts$: Observable<RelatedEntity[]>;

  public readonly ageLimits$: Observable<RelatedEntity[]>;

  public constructor(
    public readonly bookService: BookService,
    public readonly productParamsBuilderService: ProductParamsBuilderService,
  ) {
    this.genres$ = this.bookService.getRelatedEntity('genre');
    this.bookTypes$ = this.bookService.getRelatedEntity('type');
    this.ageLimits$ = this.bookService.getRelatedEntity('age-limit');
    this.coverArts$ = this.bookService.getRelatedEntity('cover-art');
    this.authors$ = this.bookService.getRelatedEntity('author');
    this.publishers$ = this.bookService.getRelatedEntity('publisher');

    this.books$ = this.paginationOptions$.asObservable().pipe(
      switchMap(pagination => {
        this.productParamsBuilderService.setPaging(pagination);
        this.config.currentPage = pagination.pageNumber;
        return this.bookService.get(this.productParamsBuilderService.params);
      }),
    );

    this.genres$ = bookService.getRelatedEntity('genre');
  }

  public ngOnInit(): void {
    this.paginationOptions$.asObservable().pipe(
      switchMap(options => {
        this.productParamsBuilderService.setPaging(options);
        return this.bookService.getQuantity(this.productParamsBuilderService.params);
      }),
    )
      .subscribe(quantity => {
      this.config.totalItems = quantity;
    });

    this.productParamsBuilderService.onParamsChanged = params => {
      this.books$ = this.bookService.get(params);
    };
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
