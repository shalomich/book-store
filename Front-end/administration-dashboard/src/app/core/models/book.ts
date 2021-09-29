
import {Product} from "./product";

export class Book extends Product{


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
    super(book);
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
