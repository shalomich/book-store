export class UserProfile {
  id: number;

  public firstName: string;

  public lastName: string;

  public email: string;

  public phoneNumber: string;

  public address: string;

  public votingPointCount: number;

  public isTelegramBotLinked: boolean;

  public basketBookIds: number[];

  public spentCurrentVotingPointCount: number;

  public currentVotedBattleBookId: number;

  constructor(profile?: UserProfile) {
    if (profile !== undefined) {
      this.id = profile.id;
      this.firstName = profile.firstName;
      this.lastName = profile.lastName;
      this.email = profile.email;
      this.phoneNumber = profile.phoneNumber;
      this.address = profile.address;
      this.votingPointCount = profile.votingPointCount;
      this.isTelegramBotLinked = profile.isTelegramBotLinked;
      this.basketBookIds = profile.basketBookIds;
      this.spentCurrentVotingPointCount = profile.spentCurrentVotingPointCount;
      this.currentVotedBattleBookId = profile.currentVotedBattleBookId;
    } else {
      this.id = 0;
      this.email = '';
      this.address = '';
      this.firstName = '';
      this.lastName = '';
      this.phoneNumber = '';
      this.votingPointCount = 0;
      this.isTelegramBotLinked = false;
      this.basketBookIds = [];
      this.spentCurrentVotingPointCount = 0;
      this.currentVotedBattleBookId = 0;
    }
  }

  public isAuthorized(): boolean {
    return !!this.id && !!this.email && !!this.firstName;
  }

  public isEqual(profile: UserProfile): boolean {
    return this.firstName === profile.firstName &&
      this.lastName === profile.lastName &&
      this.email === profile.email &&
      this.phoneNumber === profile.phoneNumber &&
      this.address === profile.address;
  }
}
