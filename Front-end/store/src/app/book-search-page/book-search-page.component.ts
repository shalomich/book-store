import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';

import { combineLatest, Observable, Subject } from 'rxjs';

import { switchMap, tap } from 'rxjs/operators';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { ActivatedRoute } from '@angular/router';

import { BookService } from '../core/services/book.service';
import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { PAGE_SIZE, SEARCH_DEPTH, SEARCH_TARGETS } from '../core/utils/values';
import { ProductPreviewSet } from '../core/models/product-preview-set';
import { SearchOptions } from '../core/interfaces/search-options';
import { OptionGroup } from '../core/interfaces/option-group';
import { UserProfile } from '../core/models/user-profile';
import { ProfileProviderService } from '../core/services/profile-provider.service';
import { SearchService } from '../core/services/search.service';

@AutoUnsubscribe()
@Component({
  selector: 'app-book-search-page',
  templateUrl: './book-search-page.component.html',
  styleUrls: ['./book-search-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BookSearchPageComponent implements OnInit, OnDestroy {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public loading = true;

  public userProfile$: Observable<UserProfile> = new Observable<UserProfile>();

  public emptyUser: UserProfile = new UserProfile();

  public constructor(
    private readonly bookService: BookService,
    private readonly searchService: SearchService,
    private route: ActivatedRoute,
    public readonly optionsStorage: ProductOptionsStorage,
    private readonly profileProviderService: ProfileProviderService,
  ) {
    this.bookSet$ = combineLatest([
      this.route.queryParams,
      this.optionsStorage.optionGroup$,
    ])
      .pipe(
        tap(() => {
          this.loading = true;
        }),
        switchMap(([params, optionGroup]) => {
          const searchValue = params.searchValue as string;
          const target = params.target as string;

          return this.findBookSet(searchValue, target, optionGroup);
         }),
        tap(() => {
          this.loading = false;
        }),
      );

    this.userProfile$ = this.profileProviderService.userProfile;
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

      return this.searchService.get(optionGroup, searchOptions);
    }

    return this.bookService.get(optionGroup);

  }

  public ngOnDestroy() {
  }
}
