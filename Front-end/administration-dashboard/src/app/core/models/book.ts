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

  publisher: RelatedEntity;

  author: RelatedEntity;

  type: RelatedEntity;

  genres: RelatedEntity[];

  originalName: string;

  ageLimit: RelatedEntity;

  coverArt: RelatedEntity;

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
    this.publisher = book.publisher;
    this.author = book.author;
    this.type = book.type;
    this.genres = book.genres;
    this.originalName = book.originalName;
    this.ageLimit = book.ageLimit;
    this.coverArt = book.coverArt;
    this.bookFormat = book.bookFormat;
    this.pageQuantity = book.pageQuantity;
  }
}
