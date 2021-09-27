import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CommonModule } from '@angular/common';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatButtonModule } from '@angular/material/button';

import { MatIconModule } from '@angular/material/icon';

import { AppComponent } from './app.component';
import { ProductTypePageComponent } from './product-type-page/product-type-page.component';
import { AppRoutingModule } from './app-routing.module';
import { ProductPageComponent } from './product-page/product-page.component';
import { ProductListItemComponent } from './product-page/product-list-item/product-list-item.component';
import { CoreModule } from './core/core.module';
import { RelatedEntityPageComponent } from './related-entity-page/related-entity-page.component';
import { RelatedEntityListItemComponent } from './related-entity-page/related-entity-list-item/related-entity-list-item.component';
import { FormModule } from './forms/form.module';

@NgModule({
  declarations: [
    AppComponent,
    ProductTypePageComponent,
    ProductPageComponent,
    ProductListItemComponent,
    RelatedEntityPageComponent,
    RelatedEntityListItemComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    BrowserAnimationsModule,
    MatExpansionModule,
    MatButtonModule,
    CommonModule,
    MatIconModule,
    FormModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
