import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { FormControl } from '@angular/forms';

import { combineLatest } from 'rxjs';

import { startWith } from 'rxjs/operators';

import { FilterParams } from '../../core/interfaces/filter-params';
import { Comparison } from '../../core/utils/comparison';

@Component({
  selector: 'app-range-filter',
  templateUrl: './range-filter.component.html',
})
export class RangeFilterComponent implements OnInit {

  public readonly lowBoundComparison = Comparison.EqualOrMore;

  public readonly highBoundComparison = Comparison.EqualOrLess;

  @Input() propertyName: string | undefined;

  @Output() filterChanged = new EventEmitter<FilterParams>();

  public readonly lowerBound: FormControl = new FormControl();

  public readonly upperBound: FormControl = new FormControl();

  constructor() {
    combineLatest([
      this.lowerBound.valueChanges,
      this.upperBound.valueChanges,
    ]).pipe(
      startWith([this.lowerBound.value, this.upperBound.value]),
    )
      .subscribe();

  }

  ngOnInit(): void {
    if (!this.propertyName) {
      throw 'Attribute property name is empty';
    }
  }
}
