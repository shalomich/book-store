import {Component, Input, OnInit} from '@angular/core';
import {SelectionService} from "../core/services/selection.service";
import {ProductParamsBuilderService} from "../core/services/product-params-builder.service";
import {ProductPreview} from "../core/models/product-preview";
import {Observable} from "rxjs";
import {SELECTION_SIZE} from "../core/utils/values";

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css']
})
export class SelectionComponent implements OnInit {

  public selection$: Observable<ProductPreview[]> = new Observable<ProductPreview[]>();

  @Input() selectionName: string | undefined
  @Input() selectionHeader: string | undefined

  constructor(
    private readonly selectionService: SelectionService,
    private readonly paramsBuilder: ProductParamsBuilderService) { }

  ngOnInit(): void {
    if (!this.selectionName)
      throw 'Attributes can not be undefined';

    this.paramsBuilder.onParamsChanged = params => this.selection$ = this.selectionService.get(this.selectionName!, params);

    this.paramsBuilder.paginationOptions$.next({
      pageSize: SELECTION_SIZE,
      pageNumber: 1
    })
  }

}
