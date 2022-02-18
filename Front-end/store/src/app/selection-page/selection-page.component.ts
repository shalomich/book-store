import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';

import { ProductParamsBuilderService } from '../core/services/product-params-builder.service';
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

  public readonly selectionName: Selection;

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public readonly disabledFilters: Array<string> = [];

  constructor(private route: ActivatedRoute,
    public paramsBuilder: ProductParamsBuilderService,
    private selectionService: SelectionService) {
    this.selectionName = route.snapshot.params.selectionName as Selection;

    this.bookSet$ = this.paramsBuilder.paginationOptions$.asObservable()
      .pipe(
        switchMap(_ => this.selectionService.get(this.selectionName, this.paramsBuilder.params)),
      );

    this.disabledFilters = this.getDisabledFilters(this.selectionName);
  }

  public getDisabledFilters(selectionName: Selection): Array<string> {
    const selection = selectionName;
    switch (selection) {
      case Selection.Novelty:
        return ['releaseYear'];
      case Selection.ForChildren:
        return ['type', 'genre', 'ageLimit'];
      default: return [];
    }
  }

  ngOnInit(): void {
    this.paramsBuilder.paginationOptions$.next({
      pageSize: PAGE_SIZE,
      pageNumber: 1,
    });
  }

}
