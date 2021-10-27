import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductImagesComponent } from './product-card/product-images/product-images.component';
import { AddToCartComponent } from './product-card/add-to-cart/add-to-cart.component';
import {CoreModule} from '../core/core.module';
import {MatButtonModule} from '@angular/material/button';
import {ProductListItemComponent} from './product-list-item/product-list-item.component';



@NgModule({
    declarations: [
        ProductImagesComponent,
        AddToCartComponent,
        ProductListItemComponent
    ],
    exports: [
        ProductImagesComponent,
        AddToCartComponent,
        ProductListItemComponent
    ],
    imports: [
        CommonModule,
        CoreModule,
        MatButtonModule
    ]
})
export class SharedModule { }
