import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { BookMapper } from '../mappers/book.mapper';
import { Book } from '../models/book';

import { BookDto } from '../DTOs/book-dto';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class BookCrudService {

  private readonly productType = 'book';

  public constructor(
    private readonly http: HttpClient,
    private readonly bookMapper: BookMapper,
    private readonly entityService: EntityService,
  ) { }

  public addBook(bookToAdd: Book): Observable<void> {
    const book = this.bookMapper.toDto(bookToAdd);
    book.id = 0;

    return this.entityService.add<BookDto>(this.productType, book);
  }

  public editBook(bookToEdit: Book): Observable<void> {
    const book = this.bookMapper.toDto(bookToEdit);

    return this.entityService.edit<BookDto>(this.productType, book.id, book);
  }

  public deleteBook(bookId: number): Observable<void> {
    return this.entityService.delete<BookDto>(this.productType, bookId);
  }

  public getSingleBook(bookId: number): Observable<Book> {
    return this.entityService.getById<BookDto>(this.productType, bookId)
      .pipe(
        map(book => this.bookMapper.fromDto(book)),
      );
  }
}
