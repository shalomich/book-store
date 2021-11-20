import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

import { FormControl } from '@angular/forms';

import { FilterComponent } from '../filter-component';

@Component({
  selector: 'app-status-filter',
  templateUrl: './status-filter.component.html',
  styleUrls: ['./status-filter.component.css'],
  providers: [{ provide: FilterComponent, useExisting: StatusFilterComponent }],
  encapsulation: ViewEncapsulation.None,
})
export class StatusFilterComponent extends FilterComponent implements OnInit {

  public readonly statusControl: FormControl = new FormControl();

  constructor() {
    super();
  }

  @Input() propertyName = '';

  @Input() label = '';

  getValue(): string | null {
    const isCheckedStatus = this.statusControl.value as boolean;

    return isCheckedStatus ? isCheckedStatus.toString() : null;
  }

  reset(): void {
    this.statusControl.reset();
  }

  public ngOnInit(): void {
    if (!this.propertyName) {
      throw 'Attribute property name is empty';
    }
  }
}
