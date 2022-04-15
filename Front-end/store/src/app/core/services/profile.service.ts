import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable, Subject} from 'rxjs';

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

  public isUserAuthorized$: Subject<boolean> = new Subject<boolean>();

  constructor(
    private readonly http: HttpClient,
    private readonly userProfileMapper: UserProfileMapper,
    private readonly authorizationDataProvider: AuthorizationDataProvider,
  ) { }

  public get userProfile(): UserProfile {
    return JSON.parse(sessionStorage.getItem('profile') ?? '') as UserProfile;
  }

  public set userProfile(profile: UserProfile) {
    sessionStorage.setItem('profile', JSON.stringify(profile));
  }

  public getUserProfile(): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.accessToken}`,
    };

    return this.http.get<UserProfileDto>(PROFILE_URL, { headers }).pipe(
      map(profile => {
        this.userProfile = this.userProfileMapper.fromDto(profile);

        this.isUserAuthorized$.next(!!profile.id);
      }),
      share(),
    );
  }

  public saveProfileChanges(profile: UserProfile): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.accessToken}`,
    };

    this.userProfile = { ...profile, id: this.userProfile.id };
    this.isUserAuthorized$.next(true);

    this.http.put<void>(PROFILE_URL, profile, { headers }).subscribe();
  }
}
