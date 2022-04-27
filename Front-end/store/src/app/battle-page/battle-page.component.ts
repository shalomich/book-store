import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { Observable } from 'rxjs';

import { CountdownConfig } from 'ngx-countdown';

import { BattleService } from '../core/services/battle.service';
import { BookBattle } from '../core/models/book-battle';

enum BookNum {
  FirstBook = 1,
  SecondBook,
}

@Component({
  selector: 'app-battle-page',
  templateUrl: './battle-page.component.html',
  styleUrls: ['./battle-page.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BattlePageComponent implements OnInit {

  public battleInfo$: Observable<BookBattle>;

  private now = new Date();

  constructor(private readonly battleService: BattleService) {
    this.battleInfo$ = this.battleService.getBattleInfo();
  }

  ngOnInit(): void {
  }

  public canUserVote(bookNumber: number, firstBookStatus: boolean, secondBookStatus: boolean): boolean {
    if (!firstBookStatus && !secondBookStatus) {
      return true;
    }

    switch (bookNumber) {
      case BookNum.FirstBook:
        return firstBookStatus;
      case BookNum.SecondBook:
        return secondBookStatus;
      default:
        return true;
    }
  }

  public getRemainingTime(endDate: Date): number {
    return (endDate.getTime() - this.now.getTime()) / 1000;
  }

}
