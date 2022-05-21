import { Image } from '../interfaces/image';
import { BattleBook } from '../interfaces/battle-book';

export interface BookBattleDto {
  readonly firstBattleBook: BattleBook;

  readonly secondBattleBook: BattleBook;

  readonly endDate: string;

  readonly state: string;

  readonly discountPercentage: number;

  readonly finalDiscount: number;
}
