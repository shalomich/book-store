import {ChangeDetectionStrategy, Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';

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

@Component({
  selector: 'app-custom-selection',
  templateUrl: './custom-selection.component.html',
  styleUrls: ['./custom-selection.component.css'],
  providers: [ProductOptionsStorage],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CustomSelectionComponent implements OnInit, OnDestroy, OnChanges {

  public bookSet$: Observable<ProductPreviewSet> = new Observable<ProductPreviewSet>();

  public selectionLink: string | undefined;

  @Input() selectionName!: Selection;

  @Input() selectionHeader: string | undefined;

  @Input()
  public userProfile: UserProfile = new UserProfile();

  private readonly subs: Subscription = new Subscription();

  constructor(
    private readonly selectionService: SelectionService,
    private readonly dialog: MatDialog,
  ) {

  }

  ngOnInit(): void {
    this.selectionLink = `book-store/catalog/selection/${this.selectionName}`;
  }

  ngOnChanges(changes: SimpleChanges) {
    const paginationOptions: PaginationOptions = {
      pageSize: SELECTION_SIZE,
      pageNumber: 1,
    };

    this.bookSet$ = this.userProfile.isAuthorized() ?
      this.selectionService.get(this.selectionName!, { pagingOptions: paginationOptions }) :
      of({} as ProductPreviewSet);
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  openSettingsDialog(): void {
    this.dialog.open(CustomSelectionSettingsDialogComponent, {
      data: { userProfile: this.userProfile },
      panelClass: 'selection-settings-dialog',
      restoreFocus: false,
      autoFocus: false,
    });
  }

}
