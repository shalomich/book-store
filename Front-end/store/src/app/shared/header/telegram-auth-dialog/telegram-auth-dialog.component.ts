import { Component, Inject, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import { TelegramAuthService } from '../../../core/services/telegram-auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {UserProfile} from '../../../core/models/user-profile';

interface DialogData {
  user: UserProfile;
}

@Component({
  selector: 'app-telegram-auth-dialog',
  templateUrl: './telegram-auth-dialog.component.html',
  styleUrls: ['./telegram-auth-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TelegramAuthDialogComponent implements OnInit, OnDestroy {

  public phoneNumberControl: FormControl = new FormControl('', Validators.required);

  private subs: Subscription = new Subscription();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<TelegramAuthDialogComponent>,
    private readonly telegramAuthService: TelegramAuthService,
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute,
  ) { }

  public ngOnInit(): void {
    this.phoneNumberControl.setValue(this.data.user.phoneNumber);
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: {},
      });

  }

  public onTelegramClick(): void {
    this.subs.add(this.telegramAuthService.getTelegramToken(this.phoneNumberControl.value, this.data.user).subscribe(data => {
      this.telegramAuthService.redirectToTelegram(data.botToken);
    }));
  }

  public onClose(): void {
    this.dialogRef.close();
  }

}
