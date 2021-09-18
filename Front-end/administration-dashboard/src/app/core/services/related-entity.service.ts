import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { RelatedEntity } from '../models/related-entity';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class RelatedEntityService extends EntityService {

  public constructor(
    http: HttpClient,
    private readonly relatedEntityMapper: RelatedEntityMapper,
  ) {
    super(http);
  }
}
