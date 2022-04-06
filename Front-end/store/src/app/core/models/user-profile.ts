export class UserProfile {
  public firstName: string;

  public lastName: string;

  public readonly email: string;

  public phoneNumber: string;

  public address: string;


  constructor(profile: UserProfile) {
    this.firstName = profile.firstName;
    this.lastName = profile.lastName;
    this.email = profile.email;
    this.phoneNumber = profile.phoneNumber;
    this.address = profile.address;
  }
}
