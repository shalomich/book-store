import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MatSelectChange } from '@angular/material/select';

import { Observable, Subject } from 'rxjs';

import { RelatedEntity } from '../../../core/models/related-entity';
import { FilterOptions } from '../../../core/interfaces/filter-options';
import {FilterComponent} from "../filter-component";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-plenty-filter',
  templateUrl: './plenty-filter.component.html',
  providers: [ {provide: FilterComponent, useExisting: PlentyFilterComponent }]
})
export class PlentyFilterComponent extends FilterComponent implements OnInit {

  public readonly idsControl: FormControl = new FormControl();

  @Input()
  public propertyName: string = '';

  @Input()
  public relatedEntities$: Observable<RelatedEntity[]> = new Observable<[]>();

  public getValue(): string | null {

    const ids = this.idsControl.value as number[];

    if (ids)
      return ids.toString();

    return null;
  }

  public reset(): void {
    this.idsControl.reset();
  }


  public ngOnInit(): void {
    if (!this.propertyName || !this.relatedEntities$) {
      throw 'Attribute property name is empty';
    }
  }
}
