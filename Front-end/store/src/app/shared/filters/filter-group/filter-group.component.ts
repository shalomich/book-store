import {Component, ContentChildren, EventEmitter, Input, OnInit, Output, QueryList} from '@angular/core';

import { MatSelectChange } from '@angular/material/select';

import {BehaviorSubject, Observable, Subject} from 'rxjs';

import { RelatedEntity } from '../../../core/models/related-entity';
import { FilterOptions } from '../../../core/interfaces/filter-options';
import {FilterComponent} from "../filter-component";
import {filter} from "rxjs/operators";
import * as objectHash from 'object-hash'

@Component({
  selector: 'filter-group',
  templateUrl: './filter-group.component.html',
})
export class FilterGroupComponent {

  @ContentChildren(FilterComponent) filterComponents: QueryList<FilterComponent> = new QueryList<FilterComponent>();

  @Input() filterOptions!: BehaviorSubject<FilterOptions>;

  private currentStateHash: string = objectHash({});

  public applyFilters(): void {

    const filterValues: any = {};

    for (let filterComponent of this.filterComponents) {
      const filterValue = filterComponent.getValue();
      if (filterValue)
        filterValues[filterComponent.propertyName] = filterValue;
    }

    const newStateHash = objectHash(filterValues);

    if (newStateHash === this.currentStateHash)
      return;

    this.currentStateHash = newStateHash;

    this.filterOptions.next({
      values: filterValues as object
    });
  }

  public resetFilters(): void {

    const filterValues = {};

    const newStateHash = objectHash(filterValues);

    if (newStateHash === this.currentStateHash)
      return;

    for (let filterComponent of this.filterComponents)
      filterComponent.reset();

    this.currentStateHash = newStateHash;

    this.filterOptions.next({
      values: filterValues
    });
  }

}

