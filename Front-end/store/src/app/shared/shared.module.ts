import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';

import { CoreModule } from '../core/core.module';

import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';


import { ProductListItemComponent } from './product-list-item/product-list-item.component';
import { RangeFilterComponent } from './filters/range-filter/range-filter.component';
import { PlentyFilterComponent } from './filters/plenty-filter/plenty-filter.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {ReactiveFormsModule} from '@angular/forms';


@NgModule({
  declarations: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
  ],
  exports: [
    ProductImagesComponent,
    AddToCartComponent,
    ProductListItemComponent,
    RangeFilterComponent,
    PlentyFilterComponent,
  ],
  imports: [
    CommonModule,
    CoreModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
})
export class SharedModule { }
