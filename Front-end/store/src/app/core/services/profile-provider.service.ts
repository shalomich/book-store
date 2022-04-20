import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { map, share } from 'rxjs/operators';

import { UserProfile } from '../models/user-profile';
import { PROFILE_URL } from '../utils/values';
import { UserProfileDto } from '../DTOs/user-profile-dto';
import { UserProfileMapper } from '../mappers/user-profile.mapper';

import { AuthorizationDataProvider } from './authorization-data.provider';
import {AuthorizationService} from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileProviderService {

  private profile: Observable<UserProfile> = new Observable<UserProfile>();

  constructor(
    private readonly http: HttpClient,
    private readonly userProfileMapper: UserProfileMapper,
    private readonly authorizationService: AuthorizationService,
  ) { }

  public get userProfile(): Observable<UserProfile> {
    return this.profile;
  }

  public getUserProfile(): Observable<UserProfile> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    this.profile = this.http.get<UserProfileDto>(PROFILE_URL, { headers }).pipe(
      map(profile => this.userProfileMapper.fromDto(profile)),
      share(),
    );

    return this.profile;
  }

  public saveProfileChanges(profileData: UserProfile): Observable<void> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.put<void>(PROFILE_URL, profileData, { headers });
  }
}
