import { Component, Input, OnInit } from '@angular/core';

import { Observable } from 'rxjs';

import { SelectionService } from '../core/services/selection.service';
import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
import { ProductPreview } from '../core/models/product-preview';
import { SELECTION_SIZE } from '../core/utils/values';
import { Selection } from '../core/enums/selection';
import {ProductPreviewSet} from "../core/models/product-preview-set";

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css'],
  providers: [ProductParamsBuilderService],
})
export class SelectionComponent implements OnInit {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public selectionLink: string | undefined;

  @Input() selectionName!: Selection;

  @Input() selectionHeader: string | undefined;

  constructor(
    private readonly selectionService: SelectionService,
    private readonly paramsBuilder: ProductParamsBuilderService,
  ) {

  }

  ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;

    this.paramsBuilder.onParamsChanged = params => this.bookSet$ = this.selectionService.get(this.selectionName!, params);

    this.paramsBuilder.paginationOptions$.next({
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    });
  }

}
