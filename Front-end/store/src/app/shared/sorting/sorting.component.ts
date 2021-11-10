import { Component, EventEmitter, Input, Output } from '@angular/core';
import {MatSelectChange} from "@angular/material/select";
import {SortingOptions} from "../../core/interfaces/sorting-options";
import {BehaviorSubject} from "rxjs";
import {_MatOptionBase, MatOptionSelectionChange} from "@angular/material/core";
import { ViewEncapsulation } from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'sorting',
  templateUrl: './sorting.component.html',
  styleUrls: ['./sorting.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SortingComponent {

  private readonly numberAttribute = 'data-number';

  private checkedOptions: Array<_MatOptionBase> = [];

  public readonly sortingSelectControl: FormControl = new FormControl();

  @Input() propertyNamesWithText: Array<[string, string]> = [];
  @Input() sortingOptions$: BehaviorSubject<Array<SortingOptions>> = new BehaviorSubject<Array<SortingOptions>>([]);

  public onOptionChanged(event: MatOptionSelectionChange) {
    const option = event.source;

    if (option.selected)
      this.addSorting(option);
    else
      this.removeSorting(option);

    this.sortingOptions$.next(this.checkedOptions.map(option => option.value as SortingOptions));
  }

  private addSorting(option: _MatOptionBase) {
    this.checkedOptions.push(option);
    option._getHostElement().setAttribute(this.numberAttribute, this.checkedOptions.length.toString());
  }

  private removeSorting(uncheckedOption: _MatOptionBase) {
    uncheckedOption._getHostElement().removeAttribute(this.numberAttribute);

    const optionIndex = this.checkedOptions.indexOf(uncheckedOption);
    this.checkedOptions.splice(optionIndex, 1);

    for (let i = optionIndex; i < this.checkedOptions.length; i++){
      const checkedOption = this.checkedOptions[i]
      let number = parseInt(checkedOption._getHostElement().getAttribute(this.numberAttribute)!);
      checkedOption._getHostElement().setAttribute(this.numberAttribute, (--number).toString())
    }
  }

  private getSortingOptions(option: _MatOptionBase) {
    return option.value as SortingOptions;
  }

  public onOrderButtonClick(propertyName: string) {
    const option = this.checkedOptions.find(option => this.getSortingOptions(option).propertyName == propertyName)!;

    if (option) {
      const sortingOptions = this.getSortingOptions(option);
      sortingOptions.isAscending = !sortingOptions.isAscending;
      this.sortingOptions$.next(this.checkedOptions.map(option => option.value as SortingOptions));
    }
  }

  public getOrder(propertyName: string) {
    const option = this.checkedOptions.find(option => this.getSortingOptions(option).propertyName == propertyName);

    return option === undefined ? true : this.getSortingOptions(option).isAscending;
  }

  public resetSortings() {
    this.checkedOptions = [];
    this.sortingOptions$.next([]);
    this.sortingSelectControl.reset();
  }
}
