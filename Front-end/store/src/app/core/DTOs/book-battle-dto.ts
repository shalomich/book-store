import { Base64Image } from '../interfaces/base64-image';
import { BattleBook } from '../interfaces/battle-book';

export interface BookBattleDto {
  readonly firstBattleBook: BattleBook;

  readonly secondBattleBook: BattleBook;

  readonly endDate: string;

  readonly isActive: boolean;

  readonly discountPercentage: number;

  readonly spentVotingPointCount: number;

  readonly votedBattleBookId: number;
}