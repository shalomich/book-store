import { Component, Input, OnDestroy, OnInit } from '@angular/core';

import { FormControl } from '@angular/forms';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';

import { FilterComponent } from '../filter-component';

@AutoUnsubscribe()
@Component({
  selector: 'app-range-filter',
  templateUrl: './range-filter.component.html',
  styleUrls: ['./range-filter.component.css'],
  providers: [{ provide: FilterComponent, useExisting: RangeFilterComponent }],
})
export class RangeFilterComponent extends FilterComponent implements OnInit, OnDestroy {

  public readonly lowerBoundControl: FormControl = new FormControl();

  @Input()
  public label = '';

  @Input() propertyName = '';

  public readonly upperBoundControl: FormControl = new FormControl();

  public getValue(): string | null {
    const lowerBoundValue = this.lowerBoundControl.value as number;
    const highBoundValue = this.upperBoundControl.value as number;

    if (!lowerBoundValue && !highBoundValue) {
      return null;
    }

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
