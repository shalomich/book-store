import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { UserProfile } from '../../core/models/user-profile';
import { SortingOptions } from '../../core/interfaces/sorting-options';
import { AuthValidator } from '../../core/validators/auth-validator';

@Component({
  selector: 'app-personal-data-form',
  templateUrl: './personal-data-form.component.html',
  styleUrls: ['./personal-data-form.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class PersonalDataFormComponent implements OnInit, OnChanges {

  @Input()
  public userData: UserProfile = new UserProfile();

  public personalDataForm = new FormGroup({
    firstName: new FormControl('', { validators: Validators.required }),
    lastName: new FormControl('', { validators: Validators.required }),
    phoneNumber: new FormControl('', { validators: [Validators.required, AuthValidator.phoneNumberFormat()] }),
    email: new FormControl('', { validators: Validators.required }),
  });

  constructor() { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.userData) {
      this.personalDataForm.setValue({
        firstName: this.userData.firstName,
        lastName: this.userData.lastName,
        phoneNumber: this.userData.phoneNumber,
        email: this.userData.email,
      });
    }
  }
}
