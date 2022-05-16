import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import {ProfileProviderService} from '../../../core/services/profile-provider.service';
import {Observable} from 'rxjs';
import {UserProfile} from '../../../core/models/user-profile';

@Component({
  selector: 'app-telegram-auth-dialog',
  templateUrl: './telegram-auth-dialog.component.html',
  styleUrls: ['./telegram-auth-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TelegramAuthDialogComponent implements OnInit {

  public phoneNumberControl: FormControl = new FormControl('', Validators.required);

  public userProfile: Observable<UserProfile> = new Observable<UserProfile>();

  constructor(private readonly profileProviderService: ProfileProviderService) {
    this.userProfile = this.profileProviderService.userProfile;
  }

  ngOnInit(): void {
  }

}
