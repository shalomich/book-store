import { Component, OnDestroy, OnInit } from '@angular/core';

import { EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { BookService } from '../../core/services/book.service';
import { Book } from '../../core/models/book';
import { BooksRelatedEntities } from '../../core/interfaces/books-related-entities';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit, OnDestroy {

  /** Film form group. */
  public bookForm: FormGroup;

  /** Observable with object with all book related entities items. */
  public readonly relatedEntities$: Observable<BooksRelatedEntities>;

  private readonly bookToEdit$: Observable<Book>;

  private readonly currentBookId: number;

  /** All subscriptions inside component. */
  private readonly subscriptions = new Subscription();

  public constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
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
      album: new FormControl(''),
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
        this.bookForm.patchValue({
          ...book,
          publisher: String(book.publisher.id),
          author: String(book.author.id),
          type: String(book.type.id),
          ageLimit: String(book.ageLimit.id),
          coverArt: String(book.coverArt.id),
          genres: book.genres.map(genre => String(genre.id)),
        });
      });
      this.subscriptions.add(sub);
    }
  }

  public ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  public handleFormSubmit(): void {
    const sub = this.relatedEntities$.subscribe(entities => {
      const book: Book = {
        ...this.bookForm.value,
        author: entities.authors.find(author => author.id === Number(this.bookForm.value.author)),
        publisher: entities.publishers.find(publisher => publisher.id === Number(this.bookForm.value.publisher)),
        type: entities.types.find(type => type.id === Number(this.bookForm.value.type)),
        coverArt: entities.coverArts.find(coverArt => coverArt.id === Number(this.bookForm.value.coverArt)),
        ageLimit: entities.ageLimits.find(ageLimit => ageLimit.id === Number(this.bookForm.value.ageLimit)),
        genres: entities.genres.filter(genre => this.bookForm.value.genres.includes(String(genre.id))),
      };
      if (this.currentBookId) {
        this.bookService.editBook(book);
      } else {
        this.bookService.addBook(book);
      }

      this.router.navigateByUrl('/dashboard/product/book');
    });
    this.subscriptions.add(sub);
  }
}
