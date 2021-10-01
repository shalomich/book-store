import { Component, OnDestroy, OnInit } from '@angular/core';

import { combineLatest, EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { map } from 'rxjs/operators';

import { BookCrudService } from '../../core/services/book-crud.service';
import { Book } from '../../core/models/book';
import { RelatedEntityCrudService } from '../../core/services/related-entity-crud.service';
import {RelatedEntity} from "../../core/models/related-entity";
import { BookConfig } from '../../core/utils/book-config';
import { Mapper } from '../../core/mappers/mapper/mapper';
import { BookDto } from '../../core/DTOs/book-dto';
import { BookMapper } from '../../core/mappers/book.mapper';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit, OnDestroy {

  public bookForm: FormGroup;

  public readonly authors$: Observable<RelatedEntity[]>;
  public readonly publishers$: Observable<RelatedEntity[]>;
  public readonly bookTypes$: Observable<RelatedEntity[]>;
  public readonly genres$: Observable<RelatedEntity[]>;
  public readonly ageLimits$: Observable<RelatedEntity[]>;
  public readonly coverArts$: Observable<RelatedEntity[]>;

  private readonly bookToEdit$: Observable<Book>;

  public readonly currentBookId: number;

  private readonly subscriptions = new Subscription();

  public constructor(
    private readonly bookService: BookCrudService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly relatedEntityService: RelatedEntityCrudService,
    private readonly bookConfig: BookConfig
  ) {
    this.currentBookId = activatedRoute.snapshot.params.id;
    this.bookToEdit$ = this.currentBookId ? this.bookService.getById(this.currentBookId) : EMPTY;

    const {AuthorConfig, PublisherConfig, BookTypeConfig, GenreConfig, AgeLimitConfig, CoverArtConfig} = bookConfig;

    this.authors$ = this.relatedEntityService.get(AuthorConfig.entityType.value);
    this.publishers$ = this.relatedEntityService.get(PublisherConfig.entityType.value);
    this.bookTypes$ = this.relatedEntityService.get(BookTypeConfig.entityType.value);
    this.genres$ = this.relatedEntityService.get(GenreConfig.entityType.value);
    this.ageLimits$ = this.relatedEntityService.get(AgeLimitConfig.entityType.value);
    this.coverArts$ = this.relatedEntityService.get(CoverArtConfig.entityType.value);

    this.bookForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required]),
      cost: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      album: new FormControl(''),
      ISBN: new FormControl('', [Validators.required]),
      releaseYear: new FormControl('', [Validators.required]),
      originalName: new FormControl(''),
      bookFormat: new FormControl(''),
      pageQuantity: new FormControl(''),
      publisherId: new FormControl('', [Validators.required]),
      authorId: new FormControl('', [Validators.required]),
      typeId: new FormControl('', [Validators.required]),
      ageLimitId: new FormControl('', [Validators.required]),
      coverArtId: new FormControl(''),
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
