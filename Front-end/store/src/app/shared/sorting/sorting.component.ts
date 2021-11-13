import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';

import { BehaviorSubject } from 'rxjs';
import { _MatOptionBase, MatOptionSelectionChange } from '@angular/material/core';
import { ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';

import { SortingOptions } from '../../core/interfaces/sorting-options';

@Component({
  selector: 'sorting',
  templateUrl: './sorting.component.html',
  styleUrls: ['./sorting.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SortingComponent {

  private readonly numberAttribute = 'data-number';

  private checkedOptions: Array<_MatOptionBase> = [];

  public readonly sortingSelectControl: FormControl = new FormControl();

  @Input() propertyNamesWithText: Array<[string, string]> = [];

  @Input() sortingOptions$: BehaviorSubject<Array<SortingOptions>> = new BehaviorSubject<Array<SortingOptions>>([]);

  public onSortingChanged(event: MatOptionSelectionChange) {
    const option = event.source;

    if (option.selected) {
      this.addSorting(option);
    } else {
      this.removeSorting(option);
    }

    this.sortingOptions$.next(this.checkedOptions.map(option => option.value as SortingOptions));
  }

  private addSorting(option: _MatOptionBase) {
    (option.value as SortingOptions).isAscending = true;

    this.checkedOptions.push(option);
    option._getHostElement().setAttribute(this.numberAttribute, this.checkedOptions.length.toString());
  }

  private removeSorting(uncheckedOption: _MatOptionBase) {
    uncheckedOption._getHostElement().removeAttribute(this.numberAttribute);

    const optionIndex = this.checkedOptions.indexOf(uncheckedOption);
    this.checkedOptions.splice(optionIndex, 1);

    for (let i = optionIndex; i < this.checkedOptions.length; i++) {
      const checkedOption = this.checkedOptions[i];
      let number = parseInt(checkedOption._getHostElement().getAttribute(this.numberAttribute)!);
      checkedOption._getHostElement().setAttribute(this.numberAttribute, (--number).toString());
    }
  }

  private getSortingOptions(propertyName: string): SortingOptions | undefined {
    return this.checkedOptions
      .map(option => option.value as SortingOptions)
      .find(sortingOptions => sortingOptions.propertyName == propertyName);
  }

  public changeDirection(propertyName: string) {
    const sortingOptions = this.getSortingOptions(propertyName)!;

    sortingOptions.isAscending = !sortingOptions.isAscending;

    this.sortingOptions$.next(this.checkedOptions.map(option => option.value as SortingOptions));
  }

  public isSortingChecked(propertyName: string): boolean {
    return this.getSortingOptions(propertyName) !== undefined;
  }

  public isAscending(propertyName: string): boolean {
    const sortingOptions = this.getSortingOptions(propertyName);

    return sortingOptions === undefined ? true : sortingOptions.isAscending!;
  }

  public resetSortings() {
    this.checkedOptions = [];
    this.sortingOptions$.next([]);
    this.sortingSelectControl.reset();
  }
}