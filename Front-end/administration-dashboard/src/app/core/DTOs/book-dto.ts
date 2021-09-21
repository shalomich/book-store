import { Album } from '../interfaces/album';

export interface BookDto {
  id: number;
  name: string;
  cost: number;
  quantity: number;
  description: string;
  album: Album;
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
