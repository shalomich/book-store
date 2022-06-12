import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewEncapsulation } from '@angular/core';

import { Observable } from 'rxjs';

import { FormControl } from '@angular/forms';

import { RelatedEntity } from '../../../core/models/related-entity';
import { FilterComponent } from '../filter-component';
import { BookService } from '../../../core/services/book.service';
import { BookFilters } from '../../../core/interfaces/book-filters';

@Component({
  selector: 'app-plenty-filter',
  templateUrl: './plenty-filter.component.html',
  styleUrls: ['./plenty-filter.component.css'],
  providers: [{ provide: FilterComponent, useExisting: PlentyFilterComponent }],
})
export class PlentyFilterComponent extends FilterComponent implements OnChanges {

  public readonly idsControl: FormControl = new FormControl();

  @Input()
  public filterData: BookFilters = {} as BookFilters;

  @Input()
  public label = '';

  @Input()
  public propertyName = '';

  public filterValues: RelatedEntity[] = [];

  public constructor() {
    super();
  }

  public getValue(): string | null {

    const ids = this.idsControl.value as number[];

    if (ids) {
      return ids.toString();
    }

    return null;
  }

  public disable(): void {
    this.idsControl.disable();
  }

  public reset(): void {
    this.idsControl.reset();
  }

  public ngOnChanges(changes: SimpleChanges) {
    if (this.filterData) {
      this.filterValues = this.filterData[this.propertyName as keyof BookFilters];
    }
  }
}
