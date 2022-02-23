import { Component, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';

import { BehaviorSubject, combineLatest, merge, Observable, Subject } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { ActivatedRoute } from '@angular/router';

import { BookService } from '../core/services/book.service';
import { Book } from '../core/models/book';
import { PaginationOptions } from '../core/interfaces/pagination-options';
import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_NUMBER, PAGE_SIZE, SEARCH_DEPTH, SEARCH_TARGETS } from '../core/utils/values';
import { RelatedEntity } from '../core/models/related-entity';
import { ProductPreviewSet } from '../core/models/product-preview-set';
import { SearchOptions } from '../core/interfaces/search-options';
import { OptionGroup } from '../core/interfaces/option-group';

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
    public readonly optionsStorage: ProductOptionsStorage,
  ) {
    this.bookSet$ = combineLatest([
      this.route.queryParams,
      this.optionsStorage.optionGroup$,
    ])
      .pipe(
        switchMap(([params, optionGroup]) => {
          const searchValue = params.searchValue as string;
          const target = params.target as string;

          return this.findBookSet(searchValue, target, optionGroup);
    }),
      );
  }

  public ngOnInit(): void {
    this.optionsStorage.setPaginationOptions({
      pageNumber: 1,
      pageSize: PAGE_SIZE,
    });
  }

  private findBookSet(searchValue: string, target: string, optionGroup: OptionGroup): Observable<ProductPreviewSet> {
    const searchTargets = SEARCH_TARGETS;

    if (searchValue && searchTargets.includes(target)) {
      const searchOptions: SearchOptions = {
        searchDepth: SEARCH_DEPTH,
        value: searchValue,
        propertyName: target,
      };

      return this.bookService.get(optionGroup, searchOptions);
    }

    return this.bookService.get(optionGroup);

  }

  public ngOnDestroy() {
  }
}
