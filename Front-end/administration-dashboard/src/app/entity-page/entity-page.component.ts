import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';

import { EntityPreview } from '../core/models/entity-preview';
import Books from '../../books.json';
import { EntityPreviewService } from '../core/services/entity-preview.service';

@Component({
  selector: 'app-entity-page',
  templateUrl: './entity-page.component.html',
  styleUrls: ['./entity-page.component.css'],
})
export class EntityPageComponent implements OnInit {

  public readonly entityName: string;

  public readonly entityList$: Observable<EntityPreview[]>;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly entityPreviewService: EntityPreviewService,
  ) {
    this.entityName = this.activatedRoute.snapshot.params.entity;

    this.entityList$ = this.entityPreviewService.getEntityPreview(this.entityName);
  }

  public ngOnInit(): void {
  }

}
