import {Component, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';

import {combineLatest, Observable} from 'rxjs';

import {switchMap} from 'rxjs/operators';

import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';

import {ActivatedRoute} from '@angular/router';

import {BookService} from '../core/services/book.service';
import {ProductOptionsStorage} from '../core/services/product-options.storage';
import {PAGE_SIZE, SEARCH_DEPTH, SEARCH_TARGETS} from '../core/utils/values';
import {ProductPreviewSet} from '../core/models/product-preview-set';
import {SearchOptions} from '../core/interfaces/search-options';
import {OptionGroup} from '../core/interfaces/option-group';

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
    if (searchValue && SEARCH_TARGETS.includes(target)) {
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
