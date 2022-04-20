import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';

import { NgxPaginationModule } from 'ngx-pagination';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatIconModule } from '@angular/material/icon';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';

import { MatFormFieldModule } from '@angular/material/form-field';

import { MatInputModule } from '@angular/material/input';

import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ProductCardPageModule } from './product-card-page/product-card-page.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { BookSearchPageComponent } from './book-search-page/book-search-page.component';
import { SelectionComponent } from './selection/selection.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SelectionPageComponent } from './selection-page/selection-page.component';
import { CatalogComponent } from './catalog/catalog.component';

import { BasketPageComponent } from './basket-page/basket-page.component';
import { BasketElementComponent } from './basket-page/basket-element/basket-element.component';
import { BasketInfoBlockComponent } from './basket-page/basket-info-block/basket-info-block.component';

import { ProductOptionsStorage } from './core/services/product-options.storage';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import {ProfileProviderService} from './core/services/profile-provider.service';


@NgModule({
  declarations: [AppComponent, BookSearchPageComponent, SelectionComponent, MainPageComponent, SelectionPageComponent, CatalogComponent, BasketPageComponent, BasketElementComponent, BasketInfoBlockComponent, ProfilePageComponent],
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
    MatExpansionModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [ProductOptionsStorage, ProfileProviderService],
  bootstrap: [AppComponent],
})
export class AppModule { }
