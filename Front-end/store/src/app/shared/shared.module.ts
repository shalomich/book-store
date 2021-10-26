import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';



@NgModule({
    declarations: [
        ProductImagesComponent,
        AddToCartComponent
    ],
    exports: [
        ProductImagesComponent
    ],
    imports: [
        CommonModule
    ]
})
export class SharedModule { }
