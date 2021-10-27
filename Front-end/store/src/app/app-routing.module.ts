import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookCardComponent } from './product-card/book-card/book-card.component';
import {ProductsSearchComponent} from './products-search/products-search.component';

const routes: Routes = [
  { path: '', redirectTo: 'book-store/catalog/book', pathMatch: 'full' },
  { path: 'book-store/catalog/book', component: ProductsSearchComponent },
  { path: 'book-store/catalog/book/:id', component: BookCardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
