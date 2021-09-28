import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';

import { IMapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class BookMapper implements IMapper<BookDto, Book> {

  /** @inheritdoc */
  public toDto(data: Book): BookDto {
    return {
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      description: data.description,
      album: data.album,
      isbn: data.ISBN,
      releaseYear: data.releaseYear,
      publisherId: data.publisherId,
      authorId: data.authorId,
      typeId: data.typeId,
      genreIds: data.genreIds,
      originalName: data.originalName,
      ageLimitId: data.ageLimitId,
      coverArtId: data.coverArtId,
      bookFormat: data.bookFormat,
      pageQuantity: data.pageQuantity,
    };
  }

  /** @inheritdoc */
  public fromDto(data: BookDto): Book {
    return new Book({
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      description: data.description,
      album: data.album,
      ISBN: data.isbn,
      releaseYear: data.releaseYear,
      publisherId: data.publisherId,
      authorId: data.authorId,
      typeId: data.typeId,
      genreIds: data.genreIds,
      originalName: data.originalName,
      ageLimitId: data.ageLimitId,
      coverArtId: data.coverArtId,
      bookFormat: data.bookFormat,
      pageQuantity: data.pageQuantity,
    });
  }
}
