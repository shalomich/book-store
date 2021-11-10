import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';

import {Observable} from 'rxjs';

import {FormControl} from '@angular/forms';

import {RelatedEntity} from '../../../core/models/related-entity';
import {FilterComponent} from '../filter-component';

@Component({
  selector: 'app-plenty-filter',
  templateUrl: './plenty-filter.component.html',
  styleUrls: ['./plenty-filter.component.css'],
  providers: [{ provide: FilterComponent, useExisting: PlentyFilterComponent }],
  encapsulation: ViewEncapsulation.None,
})
export class PlentyFilterComponent extends FilterComponent implements OnInit {
  public readonly idsControl: FormControl = new FormControl();

  @Input()
  public label = '';

  @Input()
  public propertyName = '';

  @Input()
  public relatedEntities$: Observable<RelatedEntity[]> = new Observable<[]>();

  public getValue(): string | null {

    const ids = this.idsControl.value as number[];

    if (ids) {
      return ids.toString();
    }

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
