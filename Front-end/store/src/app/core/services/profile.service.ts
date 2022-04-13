import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { map, share } from 'rxjs/operators';

import { UserProfile } from '../models/user-profile';
import { PROFILE_URL } from '../utils/values';
import { UserProfileDto } from '../DTOs/user-profile-dto';
import { UserProfileMapper } from '../mappers/user-profile.mapper';

import { AuthorizationDataProvider } from './authorization-data.provider';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {

  public userProfile: Observable<UserProfile> = new Observable<UserProfile>();

  public isUserAuthorized = false;

  constructor(
    private readonly http: HttpClient,
    private readonly userProfileMapper: UserProfileMapper,
    private readonly authorizationDataProvider: AuthorizationDataProvider,
  ) { }

  public getUserProfile(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.accessToken}`,
    };

    this.userProfile = this.http.get<UserProfileDto>(PROFILE_URL, { headers }).pipe(
      map(profile => {
        this.isUserAuthorized = !!profile.id;
        return this.userProfileMapper.fromDto(profile);
      }),
      share(),
    );
  }

  public saveProfileChanges(profile: UserProfile): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.accessToken}`,
    };

    this.http.put<void>(PROFILE_URL, profile, { headers }).subscribe();
  }
}
