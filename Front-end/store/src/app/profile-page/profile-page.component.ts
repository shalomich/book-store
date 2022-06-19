import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { ProfileProviderService } from '../core/services/profile-provider.service';
import { AuthValidator } from '../core/validators/auth-validator';
import { UserProfile } from '../core/models/user-profile';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfilePageComponent implements OnInit, OnDestroy {
  public profileForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    firstName: new FormControl('', { validators: Validators.required }),
    lastName: new FormControl(''),
    phoneNumber: new FormControl('', { validators: AuthValidator.phoneNumberFormat() }),
    address: new FormControl(''),
  });

  public allowSubmit = false;

  public loading = true;

  private currentProfile: UserProfile = new UserProfile();

  private subs: Subscription = new Subscription();

  constructor(private readonly profileService: ProfileProviderService) {
  }

  ngOnInit(): void {
    this.subs.add(this.profileService.userProfile.subscribe(profile => {
      this.currentProfile = new UserProfile(profile);

      this.profileForm.setValue({
        email: profile.email,
        firstName: profile.firstName,
        lastName: profile.lastName,
        phoneNumber: profile.phoneNumber,
        address: profile.address,
      });

      this.loading = false;
    }));

    this.subs.add(this.profileForm.valueChanges.subscribe(value => {
      this.allowSubmit = !this.currentProfile.isEqual(value);
    }));
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public onSaveChanges() {
    if (this.profileForm.valid) {
      this.profileService.saveProfileChanges(this.profileForm.value)
        .subscribe(_ => window.location.reload());
    }
  }

}
