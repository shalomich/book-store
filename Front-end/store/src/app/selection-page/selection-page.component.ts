import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ProductParamsBuilderService} from "../core/services/product-params-builder.service";
import {SelectionService} from "../core/services/selection.service";
import {Observable} from "rxjs";
import {ProductPreview} from "../core/models/product-preview";
import {PAGE_SIZE, SELECTION_SIZE} from "../core/utils/values";
import {Selection} from "../core/enums/selection";

@Component({
  selector: 'app-selection-page',
  templateUrl: './selection-page.component.html',
  styleUrls: ['./selection-page.component.css']
})
export class SelectionPageComponent implements OnInit {

  public readonly selectionName: Selection;

  public books$: Observable<ProductPreview[]> = new Observable<ProductPreview[]>();

  public readonly disabledFilters: Array<string> = [];

  constructor(private route: ActivatedRoute,
              public paramsBuilder: ProductParamsBuilderService,
              private selectionService: SelectionService) {
    this.selectionName = route.snapshot.params.selectionName as Selection;

    this.disabledFilters = this.getDisabledFilters(this.selectionName);
  }

  getDisabledFilters(selectionName: Selection): Array<string> {
    const selection = selectionName ;
    switch (selection){
      case Selection.Novelty:
        return ['releaseYear'];
      case Selection.ForChildren:
        return ['type', 'genre', 'ageLimit'];
      default: return [];
    }
  }

  ngOnInit(): void {
    this.paramsBuilder.changePageCount = params => this.selectionService.getQuantity(this.selectionName, params);

    this.paramsBuilder.onParamsChanged = params => {
      this.books$ = this.selectionService.get(this.selectionName,params);
    };

    this.paramsBuilder.paginationOptions$.next({
      pageSize: PAGE_SIZE,
      pageNumber: 1,
    });
  }

}
