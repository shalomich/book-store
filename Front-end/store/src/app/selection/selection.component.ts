import { Component, Input, OnInit } from '@angular/core';

import { Observable } from 'rxjs';

import { SelectionService } from '../core/services/selection.service';
import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { ProductPreview } from '../core/models/product-preview';
import { SELECTION_SIZE } from '../core/utils/values';
import { Selection } from '../core/enums/selection';
import {ProductPreviewSet} from "../core/models/product-preview-set";
import {PaginationOptions} from "../core/interfaces/pagination-options";

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css'],
  providers: [ProductOptionsStorage],
})
export class SelectionComponent implements OnInit {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public selectionLink: string | undefined;

  @Input() selectionName!: Selection;

  @Input() selectionHeader: string | undefined;

  constructor(
    private readonly selectionService: SelectionService,
  ) {

  }

  ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;

    const paginationOptions : PaginationOptions = {
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    }

    this.bookSet$ = this.selectionService.get(this.selectionName!, {
      pagingOptions: paginationOptions
    });
  }

}
