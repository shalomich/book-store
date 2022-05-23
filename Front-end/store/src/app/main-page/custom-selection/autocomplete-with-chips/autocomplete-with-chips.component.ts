import { Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

import { Tag } from '../../../core/interfaces/tag';

@Component({
  selector: 'app-autocomplete-with-chips',
  templateUrl: './autocomplete-with-chips.component.html',
  styleUrls: ['./autocomplete-with-chips.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AutocompleteWithChipsComponent implements OnInit {

  public separatorKeysCodes: number[] = [ENTER, COMMA];

  public tagsControl = new FormControl();

  public filteredTags: Observable<Tag[]>;

  @Input()
  public color= '';

  @Input()
  public selectedTags: Tag[] = [];

  @Input()
  public allTags: Tag[] = [];

  @Input()
  public usersTagsIds: number[] = [];

  @Input()
  onTagsListChanged: () => void = () => {};

  @ViewChild('tagInput') fruitInput!: ElementRef<HTMLInputElement>;

  constructor() {
    this.filteredTags = this.tagsControl.valueChanges.pipe(
      startWith(null),
      map((tagValue: string | null) => (tagValue ? this._filter(tagValue) : this.allTags.filter(tag => !this.selectedTags.includes(tag)))),
    );
  }

  public ngOnInit() {
    this.selectedTags = this.allTags.filter(tag => this.usersTagsIds.includes(tag.id));
  }

  public add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our tag
    if (value && this.allTags.find(tag => tag.name.toLowerCase() === value.toLowerCase())) {
      this.selectedTags.push(this.allTags.filter(tag => tag.name.toLowerCase() === value.toLowerCase())[0]);
      this.onTagsListChanged();
    }

    // Clear the input value
    event.chipInput!.clear();

    this.tagsControl.setValue(null);
  }

  public remove(tagToRemove: string): void {
    const index = this.selectedTags.indexOf(this.allTags.filter(tag => tag.name === tagToRemove)[0]);

    if (index >= 0) {
      this.selectedTags.splice(index, 1);
    }
    this.onTagsListChanged();
  }

  public selected(event: MatAutocompleteSelectedEvent): void {
    this.selectedTags.push(this.allTags.filter(tag => tag.name === event.option.viewValue)[0]);
    this.fruitInput.nativeElement.value = '';
    this.tagsControl.setValue(null);
    this.onTagsListChanged();
  }

  private _filter(value: string): Tag[] {
    const filterValue = value.toLowerCase();

    return this.allTags.filter(tag => tag.name.toLowerCase().includes(filterValue));
  }
}
