export class UserProfile {
  id: number;

  public firstName: string;

  public lastName: string;

  public email: string;

  public phoneNumber: string;

  public address: string;

  public votingPointCount: number;

  public isTelegramBotLinked: boolean;

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
    } else {
      this.id = 0;
      this.email = '';
      this.address = '';
      this.firstName = '';
      this.lastName = '';
      this.phoneNumber = '';
      this.votingPointCount = 0;
      this.isTelegramBotLinked = false;
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
