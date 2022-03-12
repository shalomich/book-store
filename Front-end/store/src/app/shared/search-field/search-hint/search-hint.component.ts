import { Component, ContentChild, Input, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable, Subject } from 'rxjs';

import { FormControl } from '@angular/forms';

import { last, map } from 'rxjs/operators';

import { SearchFieldComponent } from '../search-field.component';
import { ProductOptionsStorage } from '../../../core/services/product-options.storage';


import { EntityDto } from '../../../core/DTOs/entity-dto';
import { EntityRestService } from '../../../core/services/entity-rest.service';
import { SEARCH_DEPTH } from '../../../core/utils/values';

@Component({
  selector: 'app-search-hint',
  templateUrl: './search-hint.component.html',
  styleUrls: ['./search-hint.component.css'],
  providers: [ProductOptionsStorage],
})
export class SearchHintComponent implements OnInit {

  private readonly productName: string = 'book';

  public loading = false;

  @Input() hints: string [] = [];

  @Input() target: string | undefined;

  @Input() searchField: FormControl = new FormControl();

  @ContentChild(Text) targetText!: Text;

  constructor(
    public readonly searchInput: SearchFieldComponent,
  ) { }

  public ngOnInit(): void {

  }
}
