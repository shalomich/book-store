import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import {SearchService} from "../../core/services/search.service";
import {SEARCH_DEPTH} from "../../core/utils/values";
import {Observable} from "rxjs";
import {ProductParamsBuilderService} from "../../core/services/product-params-builder.service";
import {SearchHintsDto} from "../../core/DTOs/search-hints-dto";
import {switchMap} from "rxjs/operators";


@Component({
  selector: 'app-search-field',
  templateUrl: './search-field.component.html',
  styleUrls: ['./search-field.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SearchFieldComponent implements OnInit {

  private readonly searchUrlTemplate: string = '/book-store/catalog/book';

  private readonly defaultTarget: string = 'name';

  public input: FormControl = new FormControl();

  public searchHints$: Observable<SearchHintsDto> = new Observable<SearchHintsDto>();

  constructor(
    private readonly router: Router,
    private readonly searchService: SearchService,
    private readonly paramsBuilder: ProductParamsBuilderService
  ) { }

  ngOnInit(): void {
    this.input.valueChanges.subscribe(searchValue => {
        this.uploadHints(searchValue);
    });

    this.paramsBuilder.onParamsChanged = params => {
      this.searchHints$ = this.searchService.getHints(params)
    }
  }

  private uploadHints(searchValue: string) {
    if (searchValue) {
      this.paramsBuilder.searchOptions$.next({
        propertyName: 'name',
        value: searchValue,
        searchDepth: SEARCH_DEPTH,
      });
    }
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
