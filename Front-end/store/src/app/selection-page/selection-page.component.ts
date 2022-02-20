import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';

import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { SelectionService } from '../core/services/selection.service';
import { ProductPreview } from '../core/models/product-preview';
import { PAGE_SIZE, SELECTION_SIZE } from '../core/utils/values';
import { Selection } from '../core/enums/selection';
import { ProductPreviewSet } from '../core/models/product-preview-set';
import {switchMap} from 'rxjs/operators';

@Component({
  selector: 'app-selection-page',
  templateUrl: './selection-page.component.html',
  styleUrls: ['./selection-page.component.css'],
})
export class SelectionPageComponent implements OnInit {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public disabledFilters: Array<string> = [];

  constructor(private route: ActivatedRoute,
    public optionsStorage: ProductOptionsStorage,
    private selectionService: SelectionService) {
  }

  public getDisabledFilters(selectionName: Selection): Array<string> {
    switch (selectionName) {
      case Selection.Novelty:
        return ['releaseYear'];
      case Selection.ForChildren:
        return ['type', 'genre', 'ageLimit'];
      default: return [];
    }
  }

  ngOnInit(): void {
    const selectionName = this.route.snapshot.params.selectionName as Selection;

    this.disabledFilters = this.getDisabledFilters(selectionName);

    this.optionsStorage.optionGroup$.subscribe(optionGroup => {
      this.bookSet$ = this.selectionService.get(selectionName, optionGroup);
    });

    this.optionsStorage.setPaginationOptions({
      pageNumber: 1,
      pageSize: SELECTION_SIZE
    });
  }

}
