import { Component, EventEmitter, Input, Output } from '@angular/core';
import {MatSelectChange} from "@angular/material/select";
import {SortingOptions} from "../../core/interfaces/sorting-options";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'sorting',
  templateUrl: './sorting.component.html',
  styleUrls: ['./sorting.component.css']
})
export class SortingComponent {

  @Input() propertyNamesWithText: Array<[string, string]> = [];
  @Input() sortingOptions$: BehaviorSubject<Array<SortingOptions>> = new BehaviorSubject<Array<SortingOptions>>([]);

  public onSelectChanged(event: MatSelectChange) {
    const propertyNames = event.value as string[];

    this.sortingOptions$.next(propertyNames.map<SortingOptions>(name => {
      return {
        propertyName: name,
        isAscending: true
      }
    }));
  }

}
