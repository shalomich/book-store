import { Injectable } from '@angular/core';

import { BookDto } from '../DTOs/book-dto';
import { Book } from '../models/book';
import { RelatedEntityService } from '../services/related-entity.service';

import { SingleBookRelatedEntities } from '../interfaces/single-book-related-entities';
import { RelatedEntity } from '../models/related-entity';

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
      publisherId: data.publisher.id,
      authorId: data.author.id,
      typeId: data.type.id,
      genreIds: data.genres.map(genre => genre.id),
      originalName: data.originalName,
      ageLimitId: data.ageLimit.id,
      coverArtId: data.coverArt.id,
      bookFormat: data.bookFormat,
      pageQuantity: data.pageQuantity,
    };
  }

  /** @inheritdoc */
  public fromDto(data: BookDto, relatedEntitiesItems: SingleBookRelatedEntities): Book {
    return new Book({
      id: data.id,
      name: data.name,
      cost: data.cost,
      quantity: data.quantity,
      description: data.description,
      album: data.album,
      ISBN: data.isbn,
      releaseYear: data.releaseYear,
      publisher: relatedEntitiesItems.publisher,
      author: relatedEntitiesItems.author,
      type: relatedEntitiesItems.type,
      genres: relatedEntitiesItems.genres,
      originalName: data.originalName,
      ageLimit: relatedEntitiesItems.ageLimit,
      coverArt: relatedEntitiesItems.coverArt,
      bookFormat: data.bookFormat,
      pageQuantity: data.pageQuantity,
    });
  }
}
