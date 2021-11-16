import { Component, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_NUMBER, PAGE_SIZE } from '../core/utils/values';
import { RelatedEntity } from '../core/models/related-entity';
import {ActivatedRoute} from "@angular/router";

@AutoUnsubscribe()
@Component({
  selector: 'app-book-search-page',
  templateUrl: './book-search-page.component.html',
  styleUrls: ['./book-search-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BookSearchPageComponent implements OnInit, OnDestroy {

  public books$: Observable<ProductPreview[]> = new Observable<ProductPreview[]>();

  public readonly genres$: Observable<RelatedEntity[]>;

  public readonly bookTypes$: Observable<RelatedEntity[]>;

  public readonly coverArts$: Observable<RelatedEntity[]>;

  public readonly ageLimits$: Observable<RelatedEntity[]>;

  public readonly propertyNamesWithText: Array<[string, string]> = [['По имени', 'name'], ['По цене', 'cost'], ['По дате добавления', 'addingDate']];

  public constructor(
    private readonly bookService: BookService,
    private route: ActivatedRoute,
    public readonly productParamsBuilderService: ProductParamsBuilderService,
  ) {
    this.genres$ = this.bookService.getRelatedEntity('genre');
    this.bookTypes$ = this.bookService.getRelatedEntity('type');
    this.ageLimits$ = this.bookService.getRelatedEntity('age-limit');
    this.coverArts$ = this.bookService.getRelatedEntity('cover-art');
  }

  public ngOnInit(): void {

    this.productParamsBuilderService.changePageCount = params => this.bookService.getQuantity(params);

    this.productParamsBuilderService.onParamsChanged = params => {
      this.books$ = this.bookService.get(params);
    };

    this.route.queryParams.subscribe(params => {
      this.productParamsBuilderService.searchOptions$.next({
        propertyName: params.target,
        value: params.searchValue,
        searchDepth: 3,
      });
    });
  }

  public ngOnDestroy() {
  }
}
