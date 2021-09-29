import {ProductDto} from './product-dto';

export interface BookDto extends ProductDto{
  isbn: string;
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
}
