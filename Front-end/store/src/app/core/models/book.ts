import { Image } from '../interfaces/image';
import {CardTag} from '../interfaces/card-tag';

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

  public isbn: string;

  public quantity: number;

  public notTitleImages: Image[];

  public titleImage: Image;

  public isInBasket: boolean;

  public tags: CardTag[];


  public constructor(book: Book) {
    this.id = book.id;
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
    this.isbn = book.isbn;
    this.isInBasket = book.isInBasket;
    this.tags = book.tags;
  }
}
