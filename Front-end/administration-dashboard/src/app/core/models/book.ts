import { Album } from '../interfaces/album';

import { RelatedEntity } from './related-entity';

export class Book {
  id: number;

  name: string;

  cost: number;

  quantity: number;

  description: string;

  album: Album;

  ISBN: string;

  releaseYear: number;

  publisherId: number;

  authorId: number;

  typeId: number;

  genreIds: number[];

  originalName: string;

  ageLimitId: number;

  coverArtId: number;

  bookFormat: string;

  pageQuantity: number;


  public constructor(book: Book) {
    this.id = book.id;
    this.name = book.name;
    this.cost = book.cost;
    this.quantity = book.quantity;
    this.description = book.description;
    this.album = book.album;
    this.ISBN = book.ISBN;
    this.releaseYear = book.releaseYear;
    this.publisherId = book.publisherId;
    this.authorId = book.authorId;
    this.typeId = book.typeId;
    this.genreIds = book.genreIds;
    this.originalName = book.originalName;
    this.ageLimitId = book.ageLimitId;
    this.coverArtId = book.coverArtId;
    this.bookFormat = book.bookFormat;
    this.pageQuantity = book.pageQuantity;
  }
}
