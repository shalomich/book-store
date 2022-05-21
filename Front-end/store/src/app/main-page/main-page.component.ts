import { Component, OnDestroy, OnInit } from '@angular/core';

import { Observable, Subscription } from 'rxjs';

import { ProductOptionsStorage } from '../core/services/product-options.storage';
import { Selection } from '../core/enums/selection';
import { ProfileProviderService } from '../core/services/profile-provider.service';
import { UserProfile } from '../core/models/user-profile';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {

  public selectionEnum = Selection;

  public userProfile$: Observable<UserProfile> = new Observable<UserProfile>();

  public emptyUser: UserProfile = new UserProfile();

  private readonly subs: Subscription = new Subscription();

  constructor(private readonly profileProviderService: ProfileProviderService) {
    this.userProfile$ = this.profileProviderService.userProfile;
  }

  ngOnInit(): void {
  }

}
