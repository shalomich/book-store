import { Component, ContentChildren, Input, QueryList, ViewEncapsulation } from '@angular/core';

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
export class FilterGroupComponent {

  @ContentChildren(FilterComponent) filterComponents: QueryList<FilterComponent> = new QueryList<FilterComponent>();

  @Input() filterOptions!: BehaviorSubject<FilterOptions>;

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

    this.filterOptions.next({
      values: filterValues as object,
    });
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

    this.filterOptions.next({
      values: filterValues,
    });
  }

}

