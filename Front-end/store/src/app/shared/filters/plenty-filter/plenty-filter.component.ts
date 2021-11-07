import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MatSelectChange } from '@angular/material/select';

import { Observable, Subject } from 'rxjs';

import { RelatedEntity } from '../../../core/models/related-entity';

@Component({
  selector: 'app-plenty-filter',
  templateUrl: './plenty-filter.component.html',
})
export class PlentyFilterComponent implements OnInit {

  @Input()
  public propertyName = '';

  @Input()
  public relatedEntities$: Observable<RelatedEntity[]> = new Observable<[]>() ;

  @Input()
  public filterOptions: Map<string, string> = new Map<string, string>();

  public constructor() {
  }

  public onSelectChanged(event: MatSelectChange) {
    const values = event.value as number[];

    if (!values.length) {
      this.filterOptions.delete(this.propertyName);
    } else {
      this.filterOptions.set(this.propertyName, values.toString());
    }
  }


  public ngOnInit(): void {
    if (!this.propertyName || !this.relatedEntities$) {
      throw 'Attribute property name is empty';
    }
  }
}
