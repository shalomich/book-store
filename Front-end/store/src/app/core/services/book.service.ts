import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { BookDto } from '../DTOs/book-dto';
import { PRODUCT_URL } from '../utils/values';
import { BookMapper } from '../mappers/book.mapper';

@Injectable({
  providedIn: 'root',
})
export class BookService {

  constructor(private readonly http: HttpClient, private readonly bookMapper: BookMapper) { }

  public getById(id: number) {
    const book$ = this.http.get<BookDto>(`${PRODUCT_URL}book/${id}`);

    return book$.pipe(
      map(book => this.bookMapper.fromDto(book)),
    );
  }
}
