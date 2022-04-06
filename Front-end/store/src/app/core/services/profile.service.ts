import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { map } from 'rxjs/operators';

import { UserProfile } from '../models/user-profile';
import { PROFILE_URL } from '../utils/values';
import { UserProfileDto } from '../DTOs/user-profile-dto';
import { UserProfileMapper } from '../mappers/user-profile.mapper';
import {AuthorizationDataProvider} from './authorization-data.provider';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {

  private userProfile: BehaviorSubject<UserProfile> = new BehaviorSubject<UserProfile>({} as UserProfile);

  constructor(private readonly http: HttpClient, private readonly userProfileMapper: UserProfileMapper, private readonly authorizationDataProvider: AuthorizationDataProvider,) { }

  public getUserProfile() {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
    };

    this.http.get<UserProfileDto>(PROFILE_URL, { headers }).pipe(
      map(profile => this.userProfile.next(this.userProfileMapper.fromDto(profile))),
    )
      .subscribe();
  }
}
