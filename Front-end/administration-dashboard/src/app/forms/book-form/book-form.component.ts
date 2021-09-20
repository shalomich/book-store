import { Component, OnInit } from '@angular/core';

import { EMPTY, Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { BookService } from '../../core/services/book.service';
import { Book } from '../../core/models/book';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit {

  private readonly bookToEdit$: Observable<Book>;

  private readonly currentBookId: number;

  constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;

    this.bookToEdit$ = this.currentBookId ? this.bookService.getSingleBook(this.currentBookId) : EMPTY;
  }

  ngOnInit(): void {
    this.bookToEdit$.subscribe(book => console.log(book));
  }

}
