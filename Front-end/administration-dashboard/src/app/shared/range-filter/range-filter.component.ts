import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FilterParams} from "../../core/interfaces/filter-params";
import {Comparison} from "../../core/utils/comparison";

@Component({
  selector: 'range-filter',
  templateUrl: './range-filter.component.html'
})
export class RangeFilterComponent implements OnInit {

  public readonly lowBoundComparison = Comparison.EqualOrMore;
  public readonly highBoundComparison = Comparison.EqualOrLess;

  @Input() propertyName: string | undefined;

  @Output() filterChanged = new EventEmitter<FilterParams>();

  public onInputChanged(event: any, comparison: Comparison) {
    const value = event.target.value as string;

    const filterParams: FilterParams = {
      propertyName: this.propertyName!,
      value: value,
      comparison: comparison
    }
    this.filterChanged.emit(filterParams);
  }

  ngOnInit(): void {
    if (!this.propertyName)
      throw 'Attribute property name is empty'
  }
}
