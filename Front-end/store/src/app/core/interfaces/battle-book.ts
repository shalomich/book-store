import { Base64Image } from './base64-image';

export interface BattleBook {
  bookId: number;

  battleBookId: number;

  name: string;

  cost: number;

  titleImage: Base64Image;

  authorName: string;

  totalVotingPointCount: 0;
}
