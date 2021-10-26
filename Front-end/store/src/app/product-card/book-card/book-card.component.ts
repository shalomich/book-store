import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { Book } from '../../core/models/book';
import { BookService } from '../../core/services/book.service';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.css'],
})
export class BookCardComponent implements OnInit {

  public readonly book$: Observable<Book>;

  public readonly currentBookId: number;

  public constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;

    this.book$ = this.bookService.getById(this.currentBookId);
  }

  ngOnInit(): void {
    this.book$.subscribe(data => console.log(data));
  }

}
