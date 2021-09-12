import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import productTypeConfiguration from '../core/utils/product-type-configuration';

import { EntityPreview } from '../core/models/entity-preview';
import { EntityPreviewService } from '../core/services/entity-preview.service';

@Component({
  selector: 'app-entity-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css'],
})
export class ProductPageComponent implements OnInit {

  public readonly entityName: string | null;

  public readonly entityType: string;

  public readonly entityList$: Observable<EntityPreview[]>;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly entityPreviewService: EntityPreviewService,
  ) {
    this.entityType = this.activatedRoute.snapshot.params.product;
    this.entityName = productTypeConfiguration.getProductName(this.entityType);

    this.entityList$ = this.entityPreviewService.getEntityPreview(this.entityType);
  }

  public ngOnInit(): void {
  }

}
