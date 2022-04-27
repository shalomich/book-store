import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { Subscription } from 'rxjs';

import { UserProfile } from '../../core/models/user-profile';
import { SortingOptions } from '../../core/interfaces/sorting-options';

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
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl(''),
    email: new FormControl(''),
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
