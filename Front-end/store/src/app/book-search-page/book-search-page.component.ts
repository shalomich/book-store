import { Component, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';
import {PAGE_NUMBER, PAGE_SIZE, SEARCH_DEPTH} from '../core/utils/values';
import { RelatedEntity } from '../core/models/related-entity';
import {ActivatedRoute} from "@angular/router";
import {ProductPreviewSet} from "../core/models/product-preview-set";

@AutoUnsubscribe()
@Component({
  selector: 'app-book-search-page',
  templateUrl: './book-search-page.component.html',
  styleUrls: ['./book-search-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BookSearchPageComponent implements OnInit, OnDestroy {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public constructor(
    private readonly bookService: BookService,
    private route: ActivatedRoute,
    public readonly productParamsBuilderService: ProductParamsBuilderService,
  ) {
  }

  public ngOnInit(): void {

    this.productParamsBuilderService.onParamsChanged = params => {
      this.bookSet$ = this.bookService.get(params);
    };

    this.route.queryParams.subscribe(params => {
      this.productParamsBuilderService.searchOptions$.next({
        propertyName: params.target,
        value: params.searchValue,
        searchDepth: SEARCH_DEPTH,
      });
    });
  }

  public ngOnDestroy() {
  }
}
