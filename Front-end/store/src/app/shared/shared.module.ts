import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';

import { CoreModule } from '../core/core.module';

import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';

import {SortingComponent} from "./sorting/sorting.component";
import {MatOptionModule} from "@angular/material/core";
import { ProductListItemComponent } from './product-list-item/product-list-item.component';
import { RangeFilterComponent } from './filters/range-filter/range-filter.component';
import { PlentyFilterComponent } from './filters/plenty-filter/plenty-filter.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {FilterGroupComponent} from "./filters/filter-group/filter-group.component";
import {MatExpansionModule} from '@angular/material/expansion';
import {MatIconModule} from '@angular/material/icon';
import {PaginationPanelComponent} from "./pagination-panel/pagination-panel.component";
import {NgxPaginationModule} from "ngx-pagination";


@NgModule({
  declarations: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
    FilterGroupComponent,
    PaginationPanelComponent,
    SortingComponent
  ],
  exports: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
    FilterGroupComponent,
    PaginationPanelComponent,
    SortingComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatInputModule,
    MatExpansionModule,
    MatIconModule,
    MatInputModule,
    NgxPaginationModule,
    MatOptionModule
  ]
})
export class SharedModule { }
