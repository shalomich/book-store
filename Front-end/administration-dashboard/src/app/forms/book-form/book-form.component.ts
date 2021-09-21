import { Component, OnInit } from '@angular/core';

import { EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { BookService } from '../../core/services/book.service';
import { Book } from '../../core/models/book';
import { BooksRelatedEntities } from '../../core/interfaces/books-related-entities';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit {

  /** Film form group. */
  public bookForm: FormGroup;

  /** Observable with object with all book related entities items. */
  public readonly relatedEntities$: Observable<BooksRelatedEntities>;

  private readonly bookToEdit$: Observable<Book>;

  private readonly currentBookId: number;

  /** All subscriptions inside component. */
  private readonly subscriptions = new Subscription();

  constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;
    this.bookToEdit$ = this.currentBookId ? this.bookService.getSingleBook(this.currentBookId) : EMPTY;
    this.relatedEntities$ = this.bookService.getAllRelatedEntitiesItems();

    this.bookForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required]),
      cost: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      album: new FormControl('', [Validators.required]),
      ISBN: new FormControl('', [Validators.required]),
      releaseYear: new FormControl('', [Validators.required]),
      originalName: new FormControl('', [Validators.required]),
      bookFormat: new FormControl('', [Validators.required]),
      pageQuantity: new FormControl('', [Validators.required]),
      publisher: new FormControl('', [Validators.required]),
      author: new FormControl('', [Validators.required]),
      type: new FormControl('', [Validators.required]),
      ageLimit: new FormControl('', [Validators.required]),
      coverArt: new FormControl('', [Validators.required]),
      genres: new FormControl([], [Validators.required]),
    });
  }

  public ngOnInit(): void {
    if (this.currentBookId) {
      const sub = this.bookToEdit$?.subscribe(book => {
        this.bookForm.setValue({
          ...book,
          publisher: book.publisher.id,
          author: book.author.id,
          type: book.type.id,
          ageLimit: book.ageLimit.id,
          coverArt: book.coverArt.id,
          genres: book.genres.map(genre => String(genre.id)),
        });
        console.log(this.bookForm.value);
      });
      this.subscriptions.add(sub);
    }
  }
}
