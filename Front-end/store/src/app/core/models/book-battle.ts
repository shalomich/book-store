import { BattleBook } from '../interfaces/battle-book';

export class BookBattle {
  public readonly firstBattleBook: BattleBook;

  public readonly secondBattleBook: BattleBook;

  public readonly endDate: Date;

  constructor(battle: BookBattle) {
    this.firstBattleBook = battle.firstBattleBook;
    this.secondBattleBook = battle.secondBattleBook;
    this.endDate = battle.endDate;
  }
}
