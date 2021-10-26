import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';
import {CoreModule} from '../core/core.module';
import {MatButtonModule} from '@angular/material/button';



@NgModule({
    declarations: [
        ProductImagesComponent,
        AddToCartComponent
    ],
  exports: [
    ProductImagesComponent,
    AddToCartComponent
  ],
    imports: [
        CommonModule,
        CoreModule,
        MatButtonModule
    ]
})
export class SharedModule { }
