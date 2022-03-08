import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import {SearchService} from "../../core/services/search.service";
import {HINT_SIZE, SEARCH_DEPTH, SEARCH_TARGET_GROUP} from "../../core/utils/values";
import {Observable} from "rxjs";
import {ProductOptionsStorage} from "../../core/services/product-options.storage";
import {SearchHintsDto} from "../../core/DTOs/search-hints-dto";
import {switchMap} from "rxjs/operators";
import {SearchOptions} from "../../core/interfaces/search-options";
import {PaginationOptions} from "../../core/interfaces/pagination-options";


@Component({
  selector: 'app-search-field',
  templateUrl: './search-field.component.html',
  styleUrls: ['./search-field.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SearchFieldComponent implements OnInit {

  private readonly searchOptions: SearchOptions = {
    propertyName: 'name',
    value: '',
    searchDepth: SEARCH_DEPTH,
  }

  public readonly searchTargetGroup = SEARCH_TARGET_GROUP;

  private readonly paginationOptions: PaginationOptions = {
    pageNumber: 1,
    pageSize: HINT_SIZE
  }

  private readonly searchUrlTemplate: string = '/book-store/catalog/book';

  private readonly defaultTarget: string = 'name';

  public input: FormControl = new FormControl();

  public searchHints$: Observable<SearchHintsDto> = new Observable<SearchHintsDto>();

  constructor(
    private readonly router: Router,
    private readonly searchService: SearchService,
  ) { }

  ngOnInit(): void {
    this.input.valueChanges.subscribe(searchValue => {
        this.uploadHints(searchValue);
    });
  }

  private uploadHints(searchValue: string) {
    if (!searchValue)
      return;

    this.searchOptions.value = searchValue;

    this.searchHints$ = this.searchService.getHints(this.searchOptions, this.paginationOptions);
  }

  public buildSearchUrl(target: string, searchValue: string): string {
    if (!target || !searchValue) {
      return this.searchUrlTemplate;
    }

    return this.router
      .createUrlTree([this.searchUrlTemplate], { queryParams: { target, searchValue } })
      .toString();
  }

  public redirectToSearchPage() {
    window.location.href = this.buildSearchUrl(this.defaultTarget, this.input.value);
  }
}
