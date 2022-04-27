import { Base64Image } from '../interfaces/base64-image';
import { BattleBook } from '../interfaces/battle-book';

export interface BookBattleDto {
  readonly firstBattleBook: BattleBook;

  readonly secondBattleBook: BattleBook;

  readonly endDate: string;
}
