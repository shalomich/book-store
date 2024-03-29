import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { BehaviorSubject, Observable, Subject, Subscription } from 'rxjs';

import { map } from 'rxjs/operators';

import { PaginationInstance } from 'ngx-pagination';

import { ProductPreview } from '../../core/models/product-preview';
import { ProductOptionsStorage } from '../../core/services/product-options.storage';
import { ProductPreviewSet } from '../../core/models/product-preview-set';
import { PaginationOptions } from '../../core/interfaces/pagination-options';
import { PAGE_SIZE } from '../../core/utils/values';
import { SortingOptions } from '../../core/interfaces/sorting-options';
import { FilterOptions } from '../../core/interfaces/filter-options';
import { UserProfile } from '../../core/models/user-profile';
import { BookFilters } from '../../core/interfaces/book-filters';
import { BookService } from '../../core/services/book.service';


@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CatalogComponent implements OnInit, OnDestroy {

  public readonly propertyNamesWithText: Array<[string, string]> = [['По имени', 'name'], ['По цене', 'cost'], ['По дате добавления', 'addingDate']];

  public readonly bookFilters$: Observable<BookFilters> = new Observable<BookFilters>();

  @Input() bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  @Input() optionsStorage: ProductOptionsStorage = new ProductOptionsStorage();

  @Input() disableFilters: Array<string> = [];

  @Input()
  public userProfile: UserProfile = new UserProfile();

  @Input()
  public loading = true;

  public books: ProductPreview[] = [];

  private readonly subs: Subscription = new Subscription();

  public constructor(private readonly bookService: BookService) {
    this.bookFilters$ = this.bookService.getFilters();
  }

  public config: PaginationInstance = {
    id: 'paginationPanel',
    currentPage: 1,
    itemsPerPage: PAGE_SIZE,
    totalItems: 0,
  };

  public onSortChanged = (sortingOptions: SortingOptions[]): void => {
    this.optionsStorage.setSortingOptions(sortingOptions);
    this.config = {
      ...this.config,
      currentPage: 1,
    };
  };

  public onFilterChanged = (filterOptions: FilterOptions): void => {
    this.optionsStorage.setFilterOptions(filterOptions);
    this.config = {
      ...this.config,
      currentPage: 1,
    };
  };

  public onPageChanged(number: number): void {
    if (number === this.config.currentPage) {
      return;
    }

    this.config.currentPage = number;

    this.optionsStorage.setPaginationOptions({
      pageSize: this.config.itemsPerPage,
      pageNumber: number,
    });
  }

  public ngOnInit(): void {
    this.subs.add(this.bookSet$.subscribe(data => {
      this.books = data.previews;
      this.config = {
        ...this.config,
        totalItems: data.totalCount,
      };
    }));
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }
}
