import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MatSelectChange } from '@angular/material/select';

import { Observable, Subject } from 'rxjs';

import { Comparison } from '../../../core/utils/comparison';

import { RelatedEntity } from '../../../core/models/related-entity';
import { FilterOptions } from '../../../core/interfaces/filter-options';

@Component({
  selector: 'app-plenty-filter',
  templateUrl: './plenty-filter.component.html',
})
export class PlentyFilterComponent implements OnInit {

  private readonly comparison = Comparison.Equal;

  @Input()
  public propertyName = '';

  @Input()
  public relatedEntities$: Observable<RelatedEntity[]> = new Observable<[]>() ;

  @Input()
  public filterOptions$ = new Subject<FilterOptions>();

  public constructor() {}

  public onSelectChanged(event: MatSelectChange) {
    const values = event.value as number[];

    this.filterOptions$.next({
      propertyName: this.propertyName,
      value: values.toString(),
      comparison: this.comparison,
    });
  }

  public ngOnInit(): void {
    if (!this.propertyName || !this.relatedEntities$) {
      throw 'Attribute property name is empty';
    }
  }
}
