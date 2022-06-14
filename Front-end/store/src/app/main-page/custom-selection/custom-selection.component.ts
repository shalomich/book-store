import {
  ChangeDetectionStrategy,
  Component, EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';

import { Observable, of, Subscription } from 'rxjs';

import { switchMap } from 'rxjs/operators';

import { MatDialog } from '@angular/material/dialog';

import { SelectionService } from '../../core/services/selection.service';
import { ProductOptionsStorage } from '../../core/services/product-options.storage';
import { SELECTION_SIZE } from '../../core/utils/values';
import { Selection } from '../../core/enums/selection';
import { ProductPreviewSet } from '../../core/models/product-preview-set';
import { PaginationOptions } from '../../core/interfaces/pagination-options';
import { UserProfile } from '../../core/models/user-profile';

import {
  CustomSelectionSettingsDialogComponent,
} from './custom-selection-settings-dialog/custom-selection-settings-dialog.component';
import {
  CustomSelectionInfoDialogComponent,
} from './custom-selection-info-dialog/custom-selection-info-dialog.component';

@Component({
  selector: 'app-custom-selection',
  templateUrl: './custom-selection.component.html',
  styleUrls: ['./custom-selection.component.css'],
  providers: [ProductOptionsStorage],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CustomSelectionComponent implements OnInit, OnDestroy, OnChanges {

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

  private readonly subs: Subscription = new Subscription();

  constructor(
    private readonly selectionService: SelectionService,
    private readonly dialog: MatDialog,
  ) {
  }

  public ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;

    this.selectionLoadingStarted.emit();
  }

  public ngOnChanges(changes: SimpleChanges) {
    const paginationOptions: PaginationOptions = {
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    };

    if (!this.hasPageLoaded) {
      if (this.userProfile.isAuthorized()) {
        this.subs.add(this.selectionService.get(this.selectionName!, {
          pagingOptions: paginationOptions,
        }).subscribe(bookSet => {
          this.bookSet = bookSet;

          this.selectionLoaded.emit();
        }));
      } else {
        this.bookSet = {} as ProductPreviewSet;
        this.selectionLoaded.emit();
      }
    }
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public openSettingsDialog(): void {
    this.dialog.open(CustomSelectionSettingsDialogComponent, {
      data: { userProfile: this.userProfile },
      panelClass: 'selection-settings-dialog',
      restoreFocus: false,
      autoFocus: false,
    });
  }

  public openInfoDialog(): void {
    this.dialog.open(CustomSelectionInfoDialogComponent, {
      panelClass: 'custom-selection-info-dialog',
      restoreFocus: false,
      autoFocus: false,
    });
  }
}
