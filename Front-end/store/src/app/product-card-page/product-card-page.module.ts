import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatExpansionModule } from '@angular/material/expansion';

import { SharedModule } from '../shared/shared.module';

import { BookCardComponent } from './book-card/book-card.component';
import { BookInfoComponent } from './book-card/book-info/book-info.component';
import {MatInputModule} from '@angular/material/input';
import {MatChipsModule} from '@angular/material/chips';


@NgModule({
  declarations: [
    BookCardComponent,
    BookInfoComponent,
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    SharedModule,
    MatInputModule,
    MatChipsModule,
  ],
})
export class ProductCardPageModule { }
