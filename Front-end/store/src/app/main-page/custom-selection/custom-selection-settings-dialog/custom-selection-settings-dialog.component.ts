import { Component, Inject, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { combineLatest, Observable, Subscription } from 'rxjs';

import { map } from 'rxjs/operators';

import { TagsGroup } from '../../../core/interfaces/tags-group';
import { TagsService } from '../../../core/services/tags.service';
import { Tag } from '../../../core/interfaces/tag';
import { UserProfile } from '../../../core/models/user-profile';
import {
  CustomSelectionInfoDialogComponent
} from '../custom-selection-info-dialog/custom-selection-info-dialog.component';

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

  public universesGroupSelectedTags: Tag[] = [];

  public charactersGroupSelectedTags: Tag[] = [];

  public othersGroupSelectedTags: Tag[] = [];

  public selectedTags: Tag[] = [];

  public usersTagsIds: number[] = [];

  public selectedTagsAmount = 0;

  private subs: Subscription = new Subscription();

  constructor(
    public dialogRef: MatDialogRef<CustomSelectionSettingsDialogComponent>,
    private readonly tagsService: TagsService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) { }

  public ngOnInit(): void {
    this.subs.add(this.tagsService.getTagsGroups().subscribe(tagsGroups => {
      this.tagsGroups = tagsGroups;
    }));

    this.usersTagsIds = this.data.userProfile.tagIds;
    this.selectedTagsAmount = this.usersTagsIds.length;
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public onTagsListChanged(): () => void {
    return () => {
      this.selectedTags = this.universesGroupSelectedTags.concat(this.charactersGroupSelectedTags).concat(this.othersGroupSelectedTags);
      this.selectedTagsAmount = this.selectedTags.length;
    };
  }

  public onApply(): void {
    this.subs.add(this.tagsService.updateUsersTags(this.selectedTags.map(tag => tag.id)).subscribe(_ => window.location.reload()));
  }

  public onClose(): void {
    this.dialogRef.close();
  }

  public getUsersTags(tags: Tag[], ids: number[]): Tag[] {
    return tags.filter(tag => ids.includes(tag.id));
  }
}
