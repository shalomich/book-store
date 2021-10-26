import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FilterParams} from "../../core/interfaces/filter-params";
import {Comparison} from "../../core/utils/comparison";
import { MatSelectChange } from '@angular/material/select';
import { SortingParams } from '../../core/interfaces/sorting-params';
import { Observable } from 'rxjs';
import { RelatedEntity } from '../../core/models/related-entity';

@Component({
  selector: 'plenty-filter',
  templateUrl: './plenty-filter.component.html'
})
export class PlentyFilterComponent implements OnInit {

  private readonly comparison = Comparison.Equal;

  @Input() propertyName: string | undefined;
  @Input() relatedEntities$: Observable<RelatedEntity[]> | undefined ;

  @Output() filterChanged = new EventEmitter<FilterParams>();

  public onSelectChanged(event: MatSelectChange) {
    const values = event.value as Array<number>;

    console.log(values.toString());

    const filterParams: FilterParams = {
      propertyName: this.propertyName!,
      value: values.toString(),
      comparison: this.comparison
    }
    this.filterChanged.emit(filterParams);
  }

  ngOnInit(): void {
    if (!this.propertyName || !this.relatedEntities$)
      throw 'Attribute property name is empty'
  }
}
