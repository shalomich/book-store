import { Component, Input, OnInit } from '@angular/core';

import { RelatedEntity } from '../../core/models/related-entity';

@Component({
  selector: 'app-related-entity-list-item',
  templateUrl: './related-entity-list-item.component.html',
  styleUrls: ['./related-entity-list-item.component.css'],
})
export class RelatedEntityListItemComponent implements OnInit {

  @Input()
  public item: RelatedEntity = {} as RelatedEntity;

  public constructor() { }

  public ngOnInit(): void {
  }

}
