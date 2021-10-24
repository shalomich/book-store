import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';

import { Mapper } from './mapper/mapper';

/**
 * Mapper for film entity.
 */
@Injectable({ providedIn: 'root' })
export class BookMapper extends Mapper<BookDto, Book> {

  /** @inheritdoc */
  public toDto(data: Book): BookDto {
    return {
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      description: data.description,
      notTitleImages: data.notTitleImages,
      titleImage: data.titleImage,
      ISBN: data.ISBN,
      releaseYear: data.releaseYear,
      authorName: data.authorName,
      publisherName: data.publisherName,
      type: data.type,
      genres: data.genres,
      originalName: data.originalName,
      ageLimit: data.ageLimit,
      coverArt: data.coverArt,
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
      notTitleImages: data.notTitleImages,
      titleImage: data.titleImage,
      ISBN: data.ISBN,
      releaseYear: data.releaseYear,
      authorName: data.authorName,
      publisherName: data.publisherName,
      type: data.type,
      genres: data.genres,
      originalName: data.originalName,
      ageLimit: data.ageLimit,
      coverArt: data.coverArt,
      bookFormat: data.bookFormat,
      pageQuantity: data.pageQuantity,
    });
  }
}
