import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookCardComponent } from './product-card-page/book-card/book-card.component';
import { BookSearchPageComponent } from './book-search-page/book-search-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import {SelectionPageComponent} from "./selection-page/selection-page.component";
import {BasketPageComponent} from './basket-page/basket-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'book-store', pathMatch: 'full' },
  { path: 'book-store', component: MainPageComponent },
  { path: 'book-store/basket', component: BasketPageComponent },
  { path: 'book-store/catalog/book', component: BookSearchPageComponent },
  { path: 'book-store/catalog/book/:id', component: BookCardComponent },
  { path: 'book-store/catalog/selection/:selectionName', component: SelectionPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
