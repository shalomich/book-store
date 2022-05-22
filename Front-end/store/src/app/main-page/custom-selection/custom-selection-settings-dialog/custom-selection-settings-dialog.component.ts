import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { TagsGroup } from '../../../core/interfaces/tags-group';
import { TagsService } from '../../../core/services/tags.service';
import { Tag } from '../../../core/interfaces/tag';

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

  private subs: Subscription = new Subscription();

  constructor(
    public dialogRef: MatDialogRef<CustomSelectionSettingsDialogComponent>,
    private readonly tagsService: TagsService,
  ) { }

  ngOnInit(): void {
    this.subs.add(this.tagsService.getTagsGroups().subscribe(tagsGroups => {
      this.tagsGroups = tagsGroups;
    }));
  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

  onTagsListChanged(): () => void {
    return () => {
      this.selectedTags = this.universesGroupTags.concat(this.charactersGroupTags).concat(this.othersGroupTags);
      console.log(this.selectedTags);
    };
  }
}
