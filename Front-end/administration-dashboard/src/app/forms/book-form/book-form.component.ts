import { Component, OnDestroy, OnInit } from '@angular/core';

import { EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { BookCrudService } from '../../core/services/book-crud.service';
import { Book } from '../../core/models/book';
import { RelatedEntityCrudService } from '../../core/services/related-entity-crud.service';
import { RelatedEntity } from '../../core/models/related-entity';
import { BookConfig } from '../../core/utils/book-config';
import { BookFormValidation } from '../../core/validators/book-form-validation';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit, OnDestroy {

  public bookForm: FormGroup;

  public readonly authors$: Observable<RelatedEntity[]>;

  public readonly publishers$: Observable<RelatedEntity[]>;

  public readonly bookTypes$: Observable<RelatedEntity[]>;

  public readonly genres$: Observable<RelatedEntity[]>;

  public readonly ageLimits$: Observable<RelatedEntity[]>;

  public readonly coverArts$: Observable<RelatedEntity[]>;

  public readonly currentBookId: number;

  private readonly bookToEdit$: Observable<Book>;

  private readonly subscriptions = new Subscription();

  public constructor(
    private readonly bookService: BookCrudService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly relatedEntityService: RelatedEntityCrudService,
    private readonly bookConfig: BookConfig,
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;
    this.bookToEdit$ = this.currentBookId ? this.bookService.getById(this.currentBookId) : EMPTY;

    const { authorConfig, publisherConfig, bookTypeConfig, genreConfig, ageLimitConfig, coverArtConfig } = bookConfig;

    this.authors$ = this.relatedEntityService.get(authorConfig.entityType.value);
    this.publishers$ = this.relatedEntityService.get(publisherConfig.entityType.value);
    this.bookTypes$ = this.relatedEntityService.get(bookTypeConfig.entityType.value);
    this.genres$ = this.relatedEntityService.get(genreConfig.entityType.value);
    this.ageLimits$ = this.relatedEntityService.get(ageLimitConfig.entityType.value);
    this.coverArts$ = this.relatedEntityService.get(coverArtConfig.entityType.value);

    this.bookForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required]),
      cost: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      album: new FormControl(null),
      ISBN: new FormControl('', [Validators.required]),
      releaseYear: new FormControl('', [Validators.required]),
      originalName: new FormControl(''),
      bookFormat: new FormControl('', [Validators.required, BookFormValidation.isBookFormatValid()]),
      pageQuantity: new FormControl(''),
      publisherId: new FormControl('', [Validators.required]),
      authorId: new FormControl('', [Validators.required]),
      typeId: new FormControl('', [Validators.required]),
      ageLimitId: new FormControl('', [Validators.required]),
      coverArtId: new FormControl('', [Validators.required]),
      genreIds: new FormControl([]),
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

        this.bookForm.controls.ISBN.setAsyncValidators([BookFormValidation.isISBNValid(book.ISBN)]);
      });
      this.subscriptions.add(sub);
    }
    this.bookForm.controls.ISBN.setAsyncValidators([BookFormValidation.isISBNValid(null)]);

    this.bookForm.valueChanges.subscribe(() => {
      console.log(this.bookForm.controls.bookFormat.errors);
    });
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
      this.bookService.edit(book);
    } else {
      this.bookService.add(book);
    }

    this.router.navigateByUrl('/dashboard/product/book');
  }

  public handleDelete() {
    this.bookService.delete(this.currentBookId);
    this.router.navigateByUrl('/dashboard/product/book');
  }
}
