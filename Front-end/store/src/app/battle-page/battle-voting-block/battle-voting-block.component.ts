import { Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import {BattleService} from '../../core/services/battle.service';

@Component({
  selector: 'app-battle-voting-block',
  templateUrl: './battle-voting-block.component.html',
  styleUrls: ['./battle-voting-block.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class BattleVotingBlockComponent implements OnInit, OnDestroy {

  @Input()
  public canUserVote = false;

  @Input()
  public userMaxPoints: number = 0;

  @Input()
  public votingBookId = 0;

  @Input()
  public battleVotedBookId = 0;

  @Input()
  public onVote: (votedBookId: number, votingBookId: number, points: number) => void = () => {};

  public pointsForVote: FormControl = new FormControl();

  private subs: Subscription = new Subscription();

  constructor(private readonly battleService: BattleService) { }

  ngOnInit(): void {
    this.subs.add(this.pointsForVote.valueChanges.subscribe(value => {
      if (value < 1) {
        this.pointsForVote.setValue(1);
      } else if (value > this.userMaxPoints) {
        this.pointsForVote.setValue(this.userMaxPoints);
      }
    }));
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}
