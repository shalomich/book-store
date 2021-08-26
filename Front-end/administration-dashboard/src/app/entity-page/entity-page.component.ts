import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';

import { EntityPreview } from '../core/models/entity-preview';
import Books from '../../books.json';

@Component({
  selector: 'app-entity-page',
  templateUrl: './entity-page.component.html',
  styleUrls: ['./entity-page.component.css'],
})
export class EntityPageComponent implements OnInit {

  public readonly entityName: string;

  public readonly entityList$: Observable<EntityPreview[]>;

  public constructor(private readonly activatedRoute: ActivatedRoute) {
    this.entityName = this.activatedRoute.snapshot.params.entity;

    this.entityList$ = of(Books);
  }

  public ngOnInit(): void {
    // eslint-disable-next-line no-console
    console.log(this.entityName);
  }

}
