import { Component, EventEmitter, Input, Output } from '@angular/core';
import {MatSelectChange} from "@angular/material/select";
import {SortingOptions} from "../../core/interfaces/sorting-options";
import {BehaviorSubject} from "rxjs";
import {MatOptionSelectionChange} from "@angular/material/core";

@Component({
  selector: 'sorting',
  templateUrl: './sorting.component.html',
  styleUrls: ['./sorting.component.css']
})
export class SortingComponent {

  @Input() propertyNamesWithText: Array<[string, string]> = [];
  @Input() sortingOptions$: BehaviorSubject<Array<SortingOptions>> = new BehaviorSubject<Array<SortingOptions>>([]);

  private options: Array<SortingOptions> = []

  public onOptionChanged(event: MatOptionSelectionChange) {
    const propertyName = event.source.value;

    if (event.source.selected) {
      this.options.push({
        propertyName: propertyName,
        isAscending: true
      })
    } else {
      const removedIndex = this.options.findIndex(option => option.propertyName == propertyName);
      this.options.splice(removedIndex, 1);
     }

    this.sortingOptions$.next(this.options);
  }

  public onOrderButtonClick(propertyName: string) {
    const option = this.options.find(option => option.propertyName == propertyName)!;

    if (option) {
      option.isAscending = !option.isAscending;
      this.sortingOptions$.next(this.options);
    }
  }

  public getOrder(propertyName: string) {
    const option = this.options.find(option => option.propertyName == propertyName)!;

    return option === undefined ? true : option.isAscending;
  }
}
