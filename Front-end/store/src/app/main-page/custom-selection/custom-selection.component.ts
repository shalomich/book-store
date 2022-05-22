import {ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit} from '@angular/core';

import {Observable, of, Subscription} from 'rxjs';

import {switchMap} from 'rxjs/operators';

import {SelectionService} from '../../core/services/selection.service';
import {ProductOptionsStorage} from '../../core/services/product-options.storage';
import {SELECTION_SIZE} from '../../core/utils/values';
import {Selection} from '../../core/enums/selection';
import {ProductPreviewSet} from '../../core/models/product-preview-set';
import {PaginationOptions} from '../../core/interfaces/pagination-options';
import {UserProfile} from '../../core/models/user-profile';

@Component({
  selector: 'app-custom-selection',
  templateUrl: './custom-selection.component.html',
  styleUrls: ['./custom-selection.component.css'],
  providers: [ProductOptionsStorage],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CustomSelectionComponent implements OnInit, OnDestroy {

  public bookSet: ProductPreviewSet = {} as ProductPreviewSet;

  public selectionLink: string | undefined;

  @Input() selectionName!: Selection;

  @Input() selectionHeader: string | undefined;

  @Input()
  public userProfile$: Observable<UserProfile> = new Observable<UserProfile>();

  private readonly subs: Subscription = new Subscription();

  constructor(
    private readonly selectionService: SelectionService,
  ) {

  }

  ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;

    const paginationOptions: PaginationOptions = {
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    };

    this.subs.add(this.userProfile$.pipe(
      switchMap(profile => {
        if (profile.isAuthorized()) {
          return this.selectionService.get(this.selectionName!, { pagingOptions: paginationOptions });
        }

        return of({} as ProductPreviewSet);
      }),
    )
      .subscribe(set => {
        this.bookSet = set;
      }));
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}
