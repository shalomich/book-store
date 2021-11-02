import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ProductCardPageModule } from './product-card-page/product-card-page.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { BookSearchPageComponent } from './book-search-page/book-search-page.component';
import {NgxPaginationModule} from 'ngx-pagination';
import { BookFilterComponent } from './book-search-page/book-filter/book-filter.component';


@NgModule({
  declarations: [AppComponent, BookSearchPageComponent, BookFilterComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        ProductCardPageModule,
        CommonModule,
        CoreModule,
        SharedModule,
        HttpClientModule,
        NgxPaginationModule,
    ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
