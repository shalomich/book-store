import { Component, Inject, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import { TelegramAuthService } from '../../../core/services/telegram-auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {UserProfile} from '../../../core/models/user-profile';
import {AuthValidator} from '../../../core/validators/auth-validator';

interface DialogData {
  user: UserProfile;
  onTelegramUnlink: () => {};
}

@Component({
  selector: 'app-telegram-auth-dialog',
  templateUrl: './telegram-auth-dialog.component.html',
  styleUrls: ['./telegram-auth-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TelegramAuthDialogComponent implements OnInit, OnDestroy {

  public phoneNumberControl: FormControl = new FormControl('', { validators: [Validators.required, AuthValidator.phoneNumberFormat()], updateOn: 'blur' });

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
    sessionStorage.removeItem('openTelegramBotDialog');
  }

  public onAuthTelegramClick(): void {
    this.subs.add(this.telegramAuthService.getTelegramToken(this.phoneNumberControl.value, this.data.user).subscribe(data => {
      this.telegramAuthService.redirectToTelegramWithAuth(data.botToken);
    }));
  }

  public onTelegramClick(): void {
    this.telegramAuthService.redirectToTelegram();
  }

  public onTelegramUnlink(): void {
    this.subs.add(this.telegramAuthService.unlinkTelegramBot().subscribe(_ => {
      this.onClose();
      this.data.onTelegramUnlink();
    }));
  }

  public onClose(): void {
    sessionStorage.removeItem('openTelegramBotDialog');
    this.dialogRef.close();
  }
}
