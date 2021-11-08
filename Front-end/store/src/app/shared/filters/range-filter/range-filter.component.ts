import { Component, Input, OnDestroy, OnInit } from '@angular/core';

import { FormControl } from '@angular/forms';

import { combineLatest } from 'rxjs';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';


import { FilterOptions } from '../../../core/interfaces/filter-options';
import {FilterComponent} from "../filter-component";

@AutoUnsubscribe()
@Component({
  selector: 'app-range-filter',
  templateUrl: './range-filter.component.html',
  providers: [ {provide: FilterComponent, useExisting: RangeFilterComponent }]
})
export class RangeFilterComponent extends FilterComponent implements OnInit, OnDestroy {

  @Input() propertyName: string = '';

  public readonly lowerBoundControl: FormControl = new FormControl();

  public readonly upperBoundControl: FormControl = new FormControl();

  public getValue(): string | null {
    const lowerBoundValue = this.lowerBoundControl.value as number;
    const highBoundValue = this.upperBoundControl.value as number;

    if (!lowerBoundValue && !highBoundValue)
      return null;

    return `${lowerBoundValue ?? ''}...${highBoundValue ?? ''}`;
  }

  public reset(): void {
    this.lowerBoundControl.reset();
    this.upperBoundControl.reset();
  }

  public ngOnInit(): void {
    if (!this.propertyName) {
      throw 'Attribute property name is empty';
    }
  }

  public ngOnDestroy() {
  }
}
