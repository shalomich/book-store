import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatExpansionModule } from '@angular/material/expansion';

import { SharedModule } from '../shared/shared.module';

import { BookCardComponent } from './book-card/book-card.component';
import { BookInfoComponent } from './book-card/book-info/book-info.component';


@NgModule({
  declarations: [
    BookCardComponent,
    BookInfoComponent,
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    SharedModule,
  ],
})
export class ProductCardPageModule { }
