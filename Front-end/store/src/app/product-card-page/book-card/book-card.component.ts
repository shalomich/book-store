import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { Book } from '../../core/models/book';
import { BookService } from '../../core/services/book.service';
import { UserProfile } from '../../core/models/user-profile';
import { ProfileProviderService } from '../../core/services/profile-provider.service';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.css'],
})
export class BookCardComponent implements OnInit, OnDestroy {

  public readonly book$: Observable<Book>;

  public readonly currentBookId: number;

  public userProfile: UserProfile = new UserProfile();

  private subs: Subscription = new Subscription();

  public constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly profileProviderService: ProfileProviderService,
  ) {
    this.currentBookId = Number(activatedRoute.snapshot.params.id);

    this.book$ = this.bookService.getById(this.currentBookId);
  }

  ngOnInit(): void {
    this.subs.add(this.profileProviderService.userProfile.subscribe(profile => {
      this.userProfile = profile;
    }));
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}
