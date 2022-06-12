import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';

import { Observable, Subscription } from 'rxjs';

import { tap } from 'rxjs/operators';

import { SelectionService } from '../../core/services/selection.service';
import { ProductOptionsStorage } from '../../core/services/product-options.storage';
import { ProductPreview } from '../../core/models/product-preview';
import { SELECTION_SIZE } from '../../core/utils/values';
import { Selection } from '../../core/enums/selection';
import { ProductPreviewSet } from '../../core/models/product-preview-set';
import { PaginationOptions } from '../../core/interfaces/pagination-options';
import { UserProfile } from '../../core/models/user-profile';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css'],
  providers: [ProductOptionsStorage],
})
export class SelectionComponent implements OnInit, OnDestroy {

  public bookSet: ProductPreviewSet = {} as ProductPreviewSet;

  public selectionLink: string | undefined;

  @Input() selectionName!: Selection;

  @Input() selectionHeader: string | undefined;

  @Input()
  public userProfile: UserProfile = new UserProfile();

  @Input()
  public hasPageLoaded = false;

  @Output()
  public selectionLoadingStarted: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Output()
  public selectionLoaded: EventEmitter<boolean> = new EventEmitter<boolean>();

  private subs: Subscription = new Subscription();

  constructor(
    private readonly selectionService: SelectionService,
  ) {

  }

  public ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;

    const paginationOptions: PaginationOptions = {
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    };

    this.selectionLoadingStarted.emit();

    this.subs.add(this.selectionService.get(this.selectionName!, {
      pagingOptions: paginationOptions,
    }).subscribe(bookSet => {
      this.bookSet = bookSet;
      this.selectionLoaded.emit();
    }));
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

}
