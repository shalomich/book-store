import {AfterContentInit, Component, ContentChildren, Input, QueryList, ViewEncapsulation} from '@angular/core';

import { BehaviorSubject } from 'rxjs';

import * as objectHash from 'object-hash';

import { FilterOptions } from '../../../core/interfaces/filter-options';
import { FilterComponent } from '../filter-component';

@Component({
  selector: 'filter-group',
  templateUrl: './filter-group.component.html',
  styleUrls: ['./filter-group.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class FilterGroupComponent implements AfterContentInit{

  @ContentChildren(FilterComponent) public filterComponents: QueryList<FilterComponent> = new QueryList<FilterComponent>();

  @Input() setFiltersCallback: (filtersOptions: FilterOptions) => void = filtersOptions => {};
  @Input() disabledFilters: Array<string> = [];

  private currentStateHash: string = objectHash({});

  public applyFilters(): void {
    const filterValues: any = {};

    for (const filterComponent of this.filterComponents) {
      const filterValue = filterComponent.getValue();
      if (filterValue) {
        filterValues[filterComponent.propertyName] = filterValue;
      }
    }
    const newStateHash = objectHash(filterValues);

    if (newStateHash === this.currentStateHash) {
      return;
    }

    this.currentStateHash = newStateHash;

    this.setFiltersCallback({
      values: filterValues as object,
    });
  }

  private disableFilters(): void {
    const disabledFiltersComponents = this.filterComponents
      .filter(filterComponent => this.disabledFilters.includes(filterComponent.propertyName));
    disabledFiltersComponents.forEach(filterComponent => filterComponent.disable());
  }

  public resetFilters(): void {

    const filterValues = {};

    const newStateHash = objectHash(filterValues);

    if (newStateHash === this.currentStateHash) {
      return;
    }

    for (const filterComponent of this.filterComponents) {
      filterComponent.reset();
    }

    this.currentStateHash = newStateHash;

    this.setFiltersCallback({
      values: filterValues,
    });
  }

  ngAfterContentInit(): void {
    this.disableFilters();
  }

}

