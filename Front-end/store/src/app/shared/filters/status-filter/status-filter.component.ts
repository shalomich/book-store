import {Component, Input, OnInit} from '@angular/core';
import {FilterComponent} from "../filter-component";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-status-filter',
  templateUrl: './status-filter.component.html',
  styleUrls: ['./status-filter.component.css'],
  providers: [{ provide: FilterComponent, useExisting: StatusFilterComponent }]
})
export class StatusFilterComponent extends FilterComponent implements OnInit {

  public readonly statusControl: FormControl = new FormControl();

  constructor() {
    super();
  }

  @Input() propertyName: string = '';
  @Input() label: string = '';

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
