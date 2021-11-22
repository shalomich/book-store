import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';

import { MatOptionModule } from '@angular/material/core';

import { MatFormFieldModule } from '@angular/material/form-field';

import { MatSelectModule } from '@angular/material/select';

import { ReactiveFormsModule } from '@angular/forms';

import { MatInputModule } from '@angular/material/input';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatIconModule } from '@angular/material/icon';

import { NgxPaginationModule } from 'ngx-pagination';

import { CoreModule } from '../core/core.module';

import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';

import { SortingComponent } from './sorting/sorting.component';
import { ProductListItemComponent } from './product-list-item/product-list-item.component';
import { RangeFilterComponent } from './filters/range-filter/range-filter.component';

import { StatusFilterComponent } from './filters/status-filter/status-filter.component';
import {MatCheckboxModule} from "@angular/material/checkbox";

import {FilterGroupComponent} from "./filters/filter-group/filter-group.component";
import {PaginationPanelComponent} from "./pagination-panel/pagination-panel.component";
import { SearchFieldComponent } from './search-field/search-field.component';
import {PlentyFilterComponent} from "./filters/plenty-filter/plenty-filter.component";
import { SearchHintComponent } from './search-field/search-hint/search-hint.component';
import {RouterModule} from "@angular/router";
import { HeaderComponent } from './header/header.component';


@NgModule({
  declarations: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
    FilterGroupComponent,
    PaginationPanelComponent,
    SortingComponent,
    StatusFilterComponent,
    SearchFieldComponent,
    SearchHintComponent,
    HeaderComponent
  ],
  exports: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
    FilterGroupComponent,
    StatusFilterComponent,
    PaginationPanelComponent,
    SortingComponent,
    SearchFieldComponent,
    SearchHintComponent
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
    MatOptionModule,
    MatCheckboxModule,
    RouterModule
]
})
export class SharedModule { }
