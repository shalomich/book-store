import { Image } from '../interfaces/image';
import {CardTag} from '../interfaces/card-tag';

export interface BookDto {
  readonly id: number;

  readonly name: string;

  readonly originalName: string;

  readonly description: string;

  readonly cost: string;

  readonly isbn: string;

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

  readonly notTitleImages: Image[];

  readonly titleImage: Image;

  readonly isInBasket: boolean;

  readonly tags: CardTag[];
}
