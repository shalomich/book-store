import { Component, ContentChild, ContentChildren, Input, OnInit } from '@angular/core';

import { map } from 'rxjs/operators';

import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { HttpClient } from '@angular/common/http';

import { Form, FormControl } from '@angular/forms';

import { SearchFieldComponent } from '../search-field.component';
import { ProductParamsBuilderService } from '../../../core/services/product-params-builder.service';
import { BookService } from '../../../core/services/book.service';
import { RelatedEntityDto } from '../../../core/DTOs/related-entity-dto';

import { Entity } from '../../../core/models/entity';


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

  public entities$: Subject<EntityDto[]> = new Subject();

  @Input() target: string | undefined;

  @Input() relatedEntityName?: string;

  @Input() searchField: FormControl = new FormControl();

  @ContentChild(Text) targetText!: Text;

  constructor(
    public readonly searchInput: SearchFieldComponent,
    private readonly paramsBuilder: ProductParamsBuilderService,
    private readonly entityRestService: EntityRestService,
  ) { }

  ngOnInit(): void {
    this.paramsBuilder.onParamsChanged = params => {
      this.entityRestService
        .get(this.productName, this.relatedEntityName, params)
        .subscribe(data => this.entities$.next(data));
    };

    this.searchField.valueChanges
      .subscribe(input => this.uploadHints(input));
  }

  private uploadHints(input: string) {
    if (input) {
      this.paramsBuilder.searchOptions$.next({
        propertyName: 'name',
        value: input,
        searchDepth: SEARCH_DEPTH,
      });
    } else {
      this.entities$.next([]);
    }
  }
}
