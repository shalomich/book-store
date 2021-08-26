import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-entity-page',
  templateUrl: './entity-page.component.html',
  styleUrls: ['./entity-page.component.css'],
})
export class EntityPageComponent implements OnInit {

  private readonly entityName: string;

  public constructor(private readonly activatedRoute: ActivatedRoute) {
    this.entityName = this.activatedRoute.snapshot.params.entity;
  }

  public ngOnInit(): void {
    // eslint-disable-next-line no-console
    console.log(this.entityName);
  }

}
