import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable } from 'rxjs';

import { CountdownConfig } from 'ngx-countdown';

import { MatDialog } from '@angular/material/dialog';

import { BattleService } from '../core/services/battle.service';
import { BookBattle } from '../core/models/book-battle';
import { LoginDialogComponent } from '../shared/header/login-dialog/login-dialog.component';

import { BattleInfoDialogComponent } from './battle-info-dialog/battle-info-dialog.component';

@Component({
  selector: 'app-battle-page',
  templateUrl: './battle-page.component.html',
  styleUrls: ['./battle-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BattlePageComponent implements OnInit {

  public battleInfo$: Observable<BookBattle>;

  private now = new Date();

  constructor(private readonly battleService: BattleService, private readonly dialog: MatDialog) {
    this.battleInfo$ = this.battleService.getBattleInfo();
  }

  ngOnInit(): void {
    this.openBattleInfoDialog();
  }

  public canUserVote(bookId: number, votedBookId: number): boolean {
    if (!votedBookId) {
      return true;
    }

    return votedBookId === bookId;
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
