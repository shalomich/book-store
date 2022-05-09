import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable, Subscription } from 'rxjs';

import { CountdownConfig } from 'ngx-countdown';

import { MatDialog } from '@angular/material/dialog';

import { BattleService } from '../core/services/battle.service';
import { BookBattle } from '../core/models/book-battle';
import { LoginDialogComponent } from '../shared/header/login-dialog/login-dialog.component';

import { ProfileProviderService } from '../core/services/profile-provider.service';
import { UserProfile } from '../core/models/user-profile';

import { BattleInfoDialogComponent } from './battle-info-dialog/battle-info-dialog.component';


enum BattleStates {
  Started = 'Started',
  Extended = 'Extended',
  Finished = 'Finished',
}
@Component({
  selector: 'app-battle-page',
  templateUrl: './battle-page.component.html',
  styleUrls: ['./battle-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BattlePageComponent implements OnInit, OnDestroy {

  public battleInfo$: Observable<BookBattle>;

  public userProfile$: Observable<UserProfile>;

  private now = new Date();

  private subs: Subscription = new Subscription();

  private isAuthorized = false;

  constructor(
    private readonly battleService: BattleService,
    private readonly dialog: MatDialog,
    private readonly profileProviderService: ProfileProviderService,
  ) {
    this.battleInfo$ = this.battleService.getBattleInfo();
    this.userProfile$ = this.profileProviderService.userProfile;
  }

  ngOnInit(): void {
    this.openBattleInfoDialog();
    this.subs.add(this.userProfile$.subscribe(user => {
      this.isAuthorized = user.isAuthorized();
    }));
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  public canUserVote(bookId: number, battle: BookBattle): boolean {
    if (!this.isAuthorized || battle.state === BattleStates.Finished) {
      return false;
    }

    if (!battle.votedBattleBookId) {
      return true;
    }

    return battle.votedBattleBookId === bookId;
  }

  public getRemainingTime(endDate: Date): number {
    return (endDate.getTime() - this.now.getTime()) / 1000;
  }

  public openBattleInfoDialog(): void {
    this.dialog.open(BattleInfoDialogComponent, {
      panelClass: 'battle-info-dialog',
      autoFocus: false,
    });
  }
}
