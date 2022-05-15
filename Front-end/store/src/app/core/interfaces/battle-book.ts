import { Image } from './image';

export interface BattleBook {
  bookId: number;

  battleBookId: number;

  name: string;

  cost: number;

  titleImage: Image;

  authorName: string;

  totalVotingPointCount: 0;
}
