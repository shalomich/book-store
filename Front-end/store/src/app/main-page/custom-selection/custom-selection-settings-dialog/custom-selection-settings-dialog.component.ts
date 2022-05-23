import { Component, Inject, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { combineLatest, Observable, Subscription } from 'rxjs';

import { map } from 'rxjs/operators';

import { TagsGroup } from '../../../core/interfaces/tags-group';
import { TagsService } from '../../../core/services/tags.service';
import { Tag } from '../../../core/interfaces/tag';
import { UserProfile } from '../../../core/models/user-profile';

interface DialogData {
  userProfile: UserProfile;
}

@Component({
  selector: 'app-custom-selection-settings-dialog',
  templateUrl: './custom-selection-settings-dialog.component.html',
  styleUrls: ['./custom-selection-settings-dialog.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CustomSelectionSettingsDialogComponent implements OnInit, OnDestroy {

  public tagsGroups: TagsGroup[] = [];

  public universesGroupTags: Tag[] = [];

  public charactersGroupTags: Tag[] = [];

  public othersGroupTags: Tag[] = [];

  public selectedTags: Tag[] = [];

  public usersTagsIds: number[] = [];

  private subs: Subscription = new Subscription();

  constructor(
    public dialogRef: MatDialogRef<CustomSelectionSettingsDialogComponent>,
    private readonly tagsService: TagsService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) { }

  ngOnInit(): void {
    this.subs.add(this.tagsService.getTagsGroups().subscribe(tagsGroups => {
      this.tagsGroups = tagsGroups;
    }));

    this.usersTagsIds = this.data.userProfile.tagIds;
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  onTagsListChanged(): () => void {
    return () => {
      this.selectedTags = this.universesGroupTags.concat(this.charactersGroupTags).concat(this.othersGroupTags);
    };
  }

  public onApply(): void {
    this.subs.add(this.tagsService.updateUsersTags(this.selectedTags.map(tag => tag.id)).subscribe(_ => this.onClose()));
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
