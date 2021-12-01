import { Component, OnInit } from '@angular/core';
import {ProductParamsBuilderService} from '../core/services/product-params-builder.service';
import {Selection} from "../core/enums/selection";

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {

  public selectionEnum = Selection
  constructor() { }

  ngOnInit(): void {
  }

}
