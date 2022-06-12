import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable, Subject } from 'rxjs';

import {map, switchMap, tap} from 'rxjs/operators';

import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { SelectionService } from '../core/services/selection.service';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_SIZE, SELECTION_SIZE } from '../core/utils/values';
import { Selection } from '../core/enums/selection';
import { ProductPreviewSet } from '../core/models/product-preview-set';

@Component({
  selector: 'app-selection-page',
  templateUrl: './selection-page.component.html',
  styleUrls: ['./selection-page.component.css'],
})
export class SelectionPageComponent implements OnInit {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public loading = false;

  public disabledFilters: Array<string> = [];

  constructor(private route: ActivatedRoute,
    public optionsStorage: ProductOptionsStorage,
    private selectionService: SelectionService) {
  }

  public getDisabledFilters(selectionName: Selection): Array<string> {
    switch (selectionName) {
      case Selection.Novelty:
        return ['releaseYear'];
      default: return [];
    }
  }

  ngOnInit(): void {
    const selectionName = this.route.snapshot.params.selectionName as Selection;

    this.disabledFilters = this.getDisabledFilters(selectionName);

    this.bookSet$ = this.optionsStorage.optionGroup$.pipe(
      tap(() => {
        this.loading = true;
      }),
      switchMap(optionGroup => this.selectionService.get(selectionName, optionGroup)),
      tap(() => {
        this.loading = false;
      }),
    );

    this.optionsStorage.setPaginationOptions({
      pageNumber: 1,
      pageSize: PAGE_SIZE,
    });
  }

}
