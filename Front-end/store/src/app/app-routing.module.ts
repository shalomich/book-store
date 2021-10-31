import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookCardComponent } from './product-card-page/book-card/book-card.component';
import {BookSearchPageComponent} from './book-search-page/book-search-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'book-store/catalog/book', pathMatch: 'full' },
  { path: 'book-store/catalog/book', component: BookSearchPageComponent },
  { path: 'book-store/catalog/book/:id', component: BookCardComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
