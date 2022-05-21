import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { map, switchAll, switchMap } from 'rxjs/operators';

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

  public book: Book = {} as Book;

  public readonly currentBookId: number;

  public userProfile: UserProfile = new UserProfile();

  private subs: Subscription = new Subscription();

  public constructor(
    private readonly bookService: BookService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly profileProviderService: ProfileProviderService,
  ) {
    this.currentBookId = Number(activatedRoute.snapshot.params.id);
  }

  ngOnInit(): void {
    this.subs.add(this.profileProviderService.userProfile.subscribe(profile => {
      this.userProfile = profile;
    }));

    this.subs.add(this.bookService.getById(this.currentBookId).pipe(
      map(book => {
        this.book = book;
        if (this.userProfile.isAuthorized()) {
          return this.bookService.addBookView(this.currentBookId);
        }

        return of(null);
      }),
      switchAll(),
    )
      .subscribe());
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}
