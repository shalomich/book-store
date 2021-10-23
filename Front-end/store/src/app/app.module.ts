import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductInfoComponent } from './product-card/product-info/product-info.component';
import { ProductImagesComponent } from './product-card/product-images/product-images.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductCardComponent,
    ProductInfoComponent,
    ProductImagesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
