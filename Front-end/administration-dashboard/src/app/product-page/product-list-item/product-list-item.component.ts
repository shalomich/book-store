import { Component, Input, OnInit } from '@angular/core';

import { EntityPreview } from '../../core/models/entity-preview';

@Component({
  selector: 'app-entity-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrls: ['./product-list-item.component.css'],
})
export class ProductListItemComponent implements OnInit {

  @Input()
  public item: EntityPreview = {} as EntityPreview;

  public constructor() { }

  public ngOnInit(): void {
  }

}
