import { Component, Input, OnInit } from '@angular/core';

import { EntityPreview } from '../../core/models/entity-preview';

@Component({
  selector: 'app-entity-list-item',
  templateUrl: './entity-list-item.component.html',
  styleUrls: ['./entity-list-item.component.css'],
})
export class EntityListItemComponent implements OnInit {

  @Input()
  public item: EntityPreview = {} as EntityPreview;

  public constructor() { }

  public ngOnInit(): void {
  }

}
