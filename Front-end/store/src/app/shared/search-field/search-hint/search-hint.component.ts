import { Component, ContentChild, Input, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable, Subject } from 'rxjs';

import { FormControl } from '@angular/forms';

import { last, map } from 'rxjs/operators';

import { SearchFieldComponent } from '../search-field.component';
import { ProductParamsBuilderService } from '../../../core/services/product-params-builder.service';


import { EntityDto } from '../../../core/DTOs/entity-dto';
import { EntityRestService } from '../../../core/services/entity-rest.service';
import { SEARCH_DEPTH } from '../../../core/utils/values';

@Component({
  selector: 'app-search-hint',
  templateUrl: './search-hint.component.html',
  styleUrls: ['./search-hint.component.css'],
  providers: [ProductParamsBuilderService],
})
export class SearchHintComponent implements OnInit {

  private readonly productName: string = 'book';

  public entities$: Observable<EntityDto[] | null> = new Observable<EntityDto[] | null>();

  public loading = false;

  @Input() target: string | undefined;

  @Input() relatedEntityName?: string;

  @Input() searchField: FormControl = new FormControl();

  @ContentChild(Text) targetText!: Text;

  constructor(
    public readonly searchInput: SearchFieldComponent,
    private readonly paramsBuilder: ProductParamsBuilderService,
    private readonly entityRestService: EntityRestService,
  ) { }

  public ngOnInit(): void {
    this.paramsBuilder.onParamsChanged = params => {
      this.entities$ = this.entityRestService
        .get(this.productName, this.relatedEntityName, params)
        .pipe(
          last(),
          map(data => data.length ? data : null),
        );
    };

    this.searchField.valueChanges
      .subscribe(input => {
        this.uploadHints(input);
      });
  }

  private uploadHints(input: string) {
    if (input) {
      this.loading = true;
      this.paramsBuilder.searchOptions$.next({
        propertyName: 'name',
        value: input,
        searchDepth: SEARCH_DEPTH,
      });
    } else {
      this.entities$ = new Observable();
    }
  }
}
