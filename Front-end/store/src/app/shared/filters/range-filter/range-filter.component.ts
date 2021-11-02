import { Component, Input, OnInit } from '@angular/core';

import { FormControl } from '@angular/forms';

import {combineLatest, Subject} from 'rxjs';

import { startWith } from 'rxjs/operators';

import { Comparison } from '../../../core/utils/comparison';

import { FilterOptions } from '../../../core/interfaces/filter-options';
import {AutoUnsubscribe} from 'ngx-auto-unsubscribe';

@AutoUnsubscribe()
@Component({
  selector: 'app-range-filter',
  templateUrl: './range-filter.component.html',
})
export class RangeFilterComponent implements OnInit {

  public readonly lowBoundComparison = Comparison.EqualOrMore;

  public readonly highBoundComparison = Comparison.EqualOrLess;

  @Input() propertyName = '';

  @Input() filterOptions$ = new Subject<FilterOptions>();

  public readonly lowerBound: FormControl = new FormControl();

  public readonly upperBound: FormControl = new FormControl();

  public constructor() {
    combineLatest([
      this.lowerBound.valueChanges,
      this.upperBound.valueChanges,
    ]).pipe(
      startWith([this.lowerBound.value, this.upperBound.value]),
    )
      .subscribe(data => {
        this.filterOptions$.next({
          propertyName: this.propertyName,
          value: data[0].toString(),
          comparison: this.lowBoundComparison,
        });

        this.filterOptions$.next({
          propertyName: this.propertyName,
          value: data[1].toString(),
          comparison: this.highBoundComparison,
        });
      });

  }

  ngOnInit(): void {
    if (!this.propertyName) {
      throw 'Attribute property name is empty';
    }
  }
}
