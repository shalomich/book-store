import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookCardComponent } from './book-card/book-card.component';
import { BookInfoComponent } from './book-card/book-info/book-info.component';



@NgModule({
  declarations: [
    BookCardComponent,
    BookInfoComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ProductCardModule { }
