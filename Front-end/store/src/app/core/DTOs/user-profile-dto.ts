export interface UserProfileDto {
  id: number;

  firstName: string;

  lastName: string;

  email: string;

  phoneNumber: string;

  address: string;

  votingPointCount: number;

  isTelegramBotLinked: boolean;

  basketBookIds: number[];

  spentCurrentVotingPointCount: number;

  currentVotedBattleBookId: number;
}
