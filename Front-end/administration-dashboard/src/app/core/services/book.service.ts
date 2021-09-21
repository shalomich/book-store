import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { combineLatest, Observable } from 'rxjs';

import { map, switchMap } from 'rxjs/operators';

import { BookMapper } from '../mappers/book.mapper';
import { Book } from '../models/book';

import { BookRelatedEntitiesNames } from '../utils/values';
import { SingleBookRelatedEntities } from '../interfaces/single-book-related-entities';

import { BookDto } from '../DTOs/book-dto';

import { BooksRelatedEntities } from '../interfaces/books-related-entities';

import { EntityService } from './entity.service';
import { RelatedEntityService } from './related-entity.service';

@Injectable({
  providedIn: 'root',
})
export class BookService extends EntityService {

  private readonly productType = 'book';

  public constructor(
    http: HttpClient,
    private readonly bookMapper: BookMapper,
    private readonly relatedEntityService: RelatedEntityService,
  ) {
    super(http);
  }

  public getSingleBook(bookId: number): Observable<Book> {
    return super.getSingleEntityItem<BookDto>(this.productType, bookId).pipe(
      switchMap(book => this.getBookRelatedEntityItems(book).pipe(
        map(relatedEntitiesItems => this.bookMapper.fromDto(book, relatedEntitiesItems)),
      )),
    );
  }

  private getBookRelatedEntityItems(book: BookDto): Observable<SingleBookRelatedEntities> {
    return combineLatest([
      this.relatedEntityService.getSingleItem(BookRelatedEntitiesNames.Publisher, book.publisherId),
      this.relatedEntityService.getSingleItem(BookRelatedEntitiesNames.Author, book.authorId),
      this.relatedEntityService.getSingleItem(BookRelatedEntitiesNames.BookType, book.typeId),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Genre, book.genreIds),
      this.relatedEntityService.getSingleItem(BookRelatedEntitiesNames.AgeLimit, book.ageLimitId),
      this.relatedEntityService.getSingleItem(BookRelatedEntitiesNames.CoverArt, book.coverArtId),
    ]).pipe(
      map(([publisher, author, bookType, genres, ageLimit, coverArt]) => ({
            publisher,
            author,
            bookType,
            genres,
            ageLimit,
            coverArt,
      })),
    );
  }

  public getAllRelatedEntitiesItems(): Observable<BooksRelatedEntities> {
    return combineLatest([
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Publisher),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Author),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.BookType),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.Genre),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.AgeLimit),
      this.relatedEntityService.getItems(BookRelatedEntitiesNames.CoverArt),
    ]).pipe(
      map(([publishers, authors, bookTypes, genres, ageLimits, coverArts]) => ({
        publishers,
        authors,
        bookTypes,
        genres,
        ageLimits,
        coverArts,
      })),
    );
  }
}
