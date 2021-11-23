import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';


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

  constructor(
    private readonly router: Router,
  ) { }

  ngOnInit(): void {
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
