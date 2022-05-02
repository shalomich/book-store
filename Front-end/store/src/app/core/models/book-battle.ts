import { BattleBook } from '../interfaces/battle-book';

export class BookBattle {
  public readonly firstBattleBook: BattleBook;

  public readonly secondBattleBook: BattleBook;

  public readonly endDate: Date;

  public readonly isActive: boolean;

  public readonly discountPercentage: number;

  public readonly spentVotingPointCount: number;

  public readonly votedBattleBookId: number;

  constructor(battle: BookBattle) {
    this.firstBattleBook = battle.firstBattleBook;
    this.secondBattleBook = battle.secondBattleBook;
    this.endDate = battle.endDate;
    this.isActive = battle.isActive;
    this.discountPercentage = battle.discountPercentage;
    this.spentVotingPointCount = battle.spentVotingPointCount;
    this.votedBattleBookId = battle.votedBattleBookId;
  }
}
