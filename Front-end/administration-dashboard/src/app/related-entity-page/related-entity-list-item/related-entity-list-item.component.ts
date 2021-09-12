import { Component, Input, OnInit } from '@angular/core';

import { RelatedEntityPreview } from '../../core/models/related-entity-preview';

@Component({
  selector: 'app-related-entity-list-item',
  templateUrl: './related-entity-list-item.component.html',
  styleUrls: ['./related-entity-list-item.component.css'],
})
export class RelatedEntityListItemComponent implements OnInit {

  @Input()
  public item: RelatedEntityPreview = {} as RelatedEntityPreview;

  public constructor() { }

  public ngOnInit(): void {
  }

}
