import { Component, OnDestroy, OnInit } from '@angular/core';

import { combineLatest, EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { map } from 'rxjs/operators';

import { BookCrudService } from '../../core/services/book-crud.service';
import { Book } from '../../core/models/book';
import { BooksRelatedEntities } from '../../core/interfaces/books-related-entities';
import { RelatedEntityCrudService } from '../../core/services/related-entity-crud.service';
import { BookRelatedEntitiesNames } from '../../core/utils/values';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit, OnDestroy {

  /** Book form group. */
  public bookForm: FormGroup;

  /** Observable with object with all book related entities items. */
  public readonly relatedEntities$: Observable<BooksRelatedEntities>;

  private readonly bookToEdit$: Observable<Book>;

  private readonly currentBookId: number;

  /** All subscriptions inside component. */
  private readonly subscriptions = new Subscription();

  public constructor(
    private readonly bookService: BookCrudService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly relatedEntityService: RelatedEntityCrudService,
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;
    this.bookToEdit$ = this.currentBookId ? this.bookService.getSingleBook(this.currentBookId) : EMPTY;
    this.relatedEntities$ = combineLatest([
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Publisher),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Author),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.BookType),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Genre),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.AgeLimit),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.CoverArt),
    ]).pipe(
      map(([publishers, authors, types, genres, ageLimits, coverArts]) => ({
        publishers,
        authors,
        types,
        genres,
        ageLimits,
        coverArts,
      })),
    );

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
      publisherId: new FormControl('', [Validators.required]),
      authorId: new FormControl('', [Validators.required]),
      typeId: new FormControl('', [Validators.required]),
      ageLimitId: new FormControl('', [Validators.required]),
      coverArtId: new FormControl('', [Validators.required]),
      genreIds: new FormControl([], [Validators.required]),
    });
  }

  public ngOnInit(): void {
    if (this.currentBookId) {
      const sub = this.bookToEdit$?.subscribe(book => {
        this.bookForm.setValue({
          ...book,
          publisherId: String(book.publisherId),
          authorId: String(book.authorId),
          typeId: String(book.typeId),
          ageLimitId: String(book.ageLimitId),
          coverArtId: String(book.coverArtId),
          genreIds: book.genreIds.map(genre => String(genre)),
        });
      });
      this.subscriptions.add(sub);
    }
  }

  public ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  public handleFormSubmit(): void {
    const book: Book = {
      ...this.bookForm.value,
      authorId: this.bookForm.value.authorId,
      publisherId: this.bookForm.value.publisherId,
      typeId: this.bookForm.value.typeId,
      coverArtId: this.bookForm.value.coverArtId,
      ageLimitId: this.bookForm.value.ageLimitId,
      genreIds: this.bookForm.value.genreIds,
    };
    if (this.currentBookId) {
      this.bookService.editBook(book);
    } else {
      this.bookService.addBook(book);
    }

    this.router.navigateByUrl('/dashboard/product/book');
  }
}
