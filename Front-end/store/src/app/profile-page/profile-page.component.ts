import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';

import { ProfileProviderService } from '../core/services/profile-provider.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfilePageComponent implements OnInit {
  public profileForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl(''),
    address: new FormControl(''),
  });

  constructor(private readonly profileService: ProfileProviderService) {
  }

  ngOnInit(): void {
    this.profileService.userProfile.subscribe(profile => {
      this.profileForm.setValue({
        email: profile.email,
        firstName: profile.firstName,
        lastName: profile.lastName,
        phoneNumber: profile.phoneNumber,
        address: profile.address,
      });
    });
  }

  public onSaveChanges() {
    this.profileService.saveProfileChanges(this.profileForm.value)
      .subscribe(_ => window.location.reload());
  }

}
