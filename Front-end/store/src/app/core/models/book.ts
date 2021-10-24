import { Base64Image } from '../interfaces/base64-image';

export class Book {
  public id: number;

  public name: string;

  public originalName: string;

  public description: string;

  public cost: string;

  public authorName: string;

  public publisherName: string;

  public genres: string[];

  public releaseYear: number;

  public type: string;

  public ageLimit: string;

  public coverArt: string;

  public bookFormat: string;

  public pageQuantity: number;

  public ISBN: string;

  public quantity: number;

  public notTitleImages: Base64Image[];

  public titleImage: Base64Image;


  public constructor(book: Book) {
    this.name = book.name;
    this.originalName = book.originalName;
    this.description = book.description;
    this.cost = book.cost;
    this.authorName = book.authorName;
    this.publisherName = book.publisherName;
    this.genres = book.genres;
    this.releaseYear = book.releaseYear;
    this.type = book.type;
    this.ageLimit = book.ageLimit;
    this.coverArt = book.coverArt;
    this.bookFormat = book.bookFormat;
    this.pageQuantity = book.pageQuantity;
    this.quantity = book.quantity;
    this.notTitleImages = book.notTitleImages;
    this.titleImage = book.titleImage;
    this.ISBN = book.ISBN;
  }
}
