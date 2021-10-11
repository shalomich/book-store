import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ActivatedRoute } from '@angular/router';

import { RelatedEntity } from '../core/models/related-entity';
import { EntityPreviewService } from '../core/services/entity-preview.service';
import { ProductTypeConfigurationService } from '../core/services/product-type-configuration.service';
import { EntityRestService } from '../core/services/entity-rest.service';
import {EntityParamsBuilder} from '../core/services/entity-params.builder';
import {RELATED_ENTITY_PAGE_SIZE} from "../core/utils/values";

@Component({
  selector: 'app-related-entity-page',
  templateUrl: './related-entity-page.component.html',
  styleUrls: ['./related-entity-page.component.css'],
})
export class RelatedEntityPageComponent implements OnInit {

  public readonly entityName: string | undefined;

  public readonly productType: string;

  public readonly entityType: string;

  public entityList$: Observable<RelatedEntity[]>;

  public pageCount$: Observable<number>;

  public isValid = true;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly previewService: EntityPreviewService,
    private readonly paramsBuilder: EntityParamsBuilder,
    productTypeConfiguration : ProductTypeConfigurationService
  ) {
    this.entityType = this.activatedRoute.snapshot.params.relatedEntity;
    this.productType = this.activatedRoute.snapshot.params.product;

    this.entityName = productTypeConfiguration.getRelatedEntityName(this.productType, this.entityType);

    this.paramsBuilder.setPagging(RELATED_ENTITY_PAGE_SIZE);
    this.entityList$ = this.previewService.getRelatedEntityPreviews(this.entityType, paramsBuilder.params);
    this.pageCount$ = previewService.getPreviewPageCount(this.entityType, paramsBuilder.params);
  }

  public ngOnInit(): void {
    if (!this.entityName) {
      this.isValid = false;
    }
  }

  public pageChanged(pageNumber: number){
    this.paramsBuilder.setPagging(RELATED_ENTITY_PAGE_SIZE,pageNumber)
    this.entityList$ = this.previewService.getRelatedEntityPreviews(this.entityType, this.paramsBuilder.params);
  }
}
