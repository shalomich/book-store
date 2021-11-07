import { Component, Input, OnDestroy, OnInit } from '@angular/core';

import { FormControl } from '@angular/forms';

import { combineLatest } from 'rxjs';

import { AutoUnsubscribe } from 'ngx-auto-unsubscribe';


@AutoUnsubscribe()
@Component({
  selector: 'app-range-filter',
  templateUrl: './range-filter.component.html',
})
export class RangeFilterComponent implements OnInit, OnDestroy {

  @Input() propertyName = '';

  @Input() filterOptions = new Map<string, string>();

  public readonly lowerBound: FormControl = new FormControl();

  public readonly upperBound: FormControl = new FormControl();

  public constructor() {
    combineLatest([
      this.lowerBound.valueChanges,
      this.upperBound.valueChanges,
    ])
      .subscribe(data => {
        if (data[0] === null || data[1] === null) {
          this.filterOptions.delete(this.propertyName);
        } else {
          this.filterOptions.set(this.propertyName, `${data[0]}...${data[1]}`);
        }
      });

  }

  public ngOnInit(): void {
    if (!this.propertyName) {
      throw 'Attribute property name is empty';
    }
  }

  public ngOnDestroy() {
  }
}
