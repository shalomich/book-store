import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import productTypeConfiguration from '../core/utils/product-type-configuration';
import { RelatedEntity } from '../core/models/related-entity';
import { PaginationService } from '../core/services/pagination.service';

@Component({
  selector: 'app-related-entity-page',
  templateUrl: './related-entity-page.component.html',
  styleUrls: ['./related-entity-page.component.css'],
})
export class RelatedEntityPageComponent implements OnInit {

  public readonly entityName: string | undefined;

  public readonly productType: string;

  public readonly entityType: string;

  public readonly entityList$: Observable<RelatedEntity[]>;

  /** URL validation status. */
  public isValid = true;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly paginationService: PaginationService,
  ) {
    this.entityType = this.activatedRoute.snapshot.params.relatedEntity;
    this.productType = this.activatedRoute.snapshot.params.product;

    this.entityName = productTypeConfiguration.getRelatedEntityName(this.productType, this.entityType);

    this.entityList$ = this.paginationService.getRelatedEntityPage(this.entityType);
  }

  public ngOnInit(): void {
    if (!this.entityName) {
      this.isValid = false;
    }
  }

}
