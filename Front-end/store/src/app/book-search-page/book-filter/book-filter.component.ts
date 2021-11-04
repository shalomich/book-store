import { Component, Input, OnInit } from '@angular/core';

import { Subject } from 'rxjs';

import { FilterOptions } from '../../core/interfaces/filter-options';

@Component({
  selector: 'app-book-filter',
  templateUrl: './book-filter.component.html',
  styleUrls: ['./book-filter.component.css'],
})
export class BookFilterComponent implements OnInit {

  @Input()
  public filterOptions$: Subject<FilterOptions> = new Subject<FilterOptions>();

  @Input()
  public onApply: Function = () => {};

  public constructor() {
  }

  public ngOnInit(): void {
  }

  public onApplyFilters() {
    this.onApply();
  }
}
