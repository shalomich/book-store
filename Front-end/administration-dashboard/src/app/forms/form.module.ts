import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule, FormsModule as Forms } from '@angular/forms';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { RouterModule } from '@angular/router';

import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

import { BookFormComponent } from './book-form/book-form.component';
import { RelatedEntityFormComponent } from './related-entity-form/related-entity-form.component';


@NgModule({
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    Forms,
    ReactiveFormsModule,
    RouterModule,
    MatSelectModule,
    MatOptionModule,
  ],
  declarations: [BookFormComponent, RelatedEntityFormComponent],
  exports: [MatFormFieldModule],
})
export class FormModule { }
