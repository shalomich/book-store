import { Base64Image } from '../interfaces/base64-image';

export interface BookDto {
  readonly id: number;

  readonly name: string;

  readonly originalName: string;

  readonly description: string;

  readonly cost: string;

  readonly authorName: string;

  readonly publisherName: string;

  readonly genres: string[];

  readonly releaseYear: number;

  readonly type: string;

  readonly ageLimit: string;

  readonly coverArt: string;

  readonly bookFormat: string;

  readonly pageQuantity: number;

  readonly quantity: number;

  readonly notTitleImages: Base64Image[];

  readonly titleImage: Base64Image;
}
