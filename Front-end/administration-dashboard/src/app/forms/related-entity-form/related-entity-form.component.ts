import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EMPTY, Observable, Subscription } from 'rxjs';

import { ActivatedRoute, Router } from '@angular/router';

import { RelatedEntity } from '../../core/models/related-entity';
import { RelatedEntityCrudService } from '../../core/services/related-entity-crud.service';

@Component({
  selector: 'app-related-entity-form',
  templateUrl: './related-entity-form.component.html',
  styleUrls: ['./related-entity-form.component.css'],
})
export class RelatedEntityFormComponent implements OnInit {

  /** Book form group. */
  public relatedEntityForm: FormGroup;

  private readonly entityItemToEdit$: Observable<RelatedEntity>;

  public readonly currentId: number;

  public readonly entityName: string;

  /** All subscriptions inside component. */
  private readonly subscriptions = new Subscription();

  public constructor(
    private readonly relatedEntityService: RelatedEntityCrudService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
  ) {
    this.currentId = activatedRoute.snapshot.params.id;
    this.entityName = activatedRoute.snapshot.params.relatedEntity;
    this.entityItemToEdit$ = this.currentId ? this.relatedEntityService.getSingleItem(this.entityName, this.currentId) : EMPTY;

    this.relatedEntityForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required]),
    });
  }

  public ngOnInit(): void {
    if (this.currentId) {
      const sub = this.entityItemToEdit$?.subscribe(item => {
        this.relatedEntityForm.setValue({
          ...item,
        });
      });
      this.subscriptions.add(sub);
    }
  }

  public ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  public handleFormSubmit(): void {
    const item: RelatedEntity = {
      ...this.relatedEntityForm.value,
    };
    if (this.currentId) {
      this.relatedEntityService.editRelatedEntityItem(item, this.entityName);
    } else {
      this.relatedEntityService.addRelatedEntityItem(item, this.entityName);
    }

    this.router.navigateByUrl(`/dashboard/product/book/${this.entityName}`);
  }

  public handleDelete() {
    this.relatedEntityService.deleteRelatedEntityItem(this.entityName, this.currentId);
    this.router.navigateByUrl(`/dashboard/product/book/${this.entityName}`);
  }

}
