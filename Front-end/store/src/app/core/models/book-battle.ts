import { BattleBook } from '../interfaces/battle-book';

export class BookBattle {
  public readonly firstBattleBook: BattleBook;

  public readonly secondBattleBook: BattleBook;

  public readonly endDate: Date;

  public readonly state: string;

  public readonly discountPercentage: number;

  public readonly spentVotingPointCount: number;

  public readonly votedBattleBookId: number;

  public readonly finalDiscount: number;

  constructor(battle: BookBattle) {
    this.firstBattleBook = battle.firstBattleBook;
    this.secondBattleBook = battle.secondBattleBook;
    this.endDate = battle.endDate;
    this.state = battle.state;
    this.discountPercentage = battle.discountPercentage;
    this.spentVotingPointCount = battle.spentVotingPointCount;
    this.votedBattleBookId = battle.votedBattleBookId;
    this.finalDiscount = battle.finalDiscount;
  }
}
