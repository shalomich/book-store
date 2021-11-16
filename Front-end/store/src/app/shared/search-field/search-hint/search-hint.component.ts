import {Component, ContentChild, ContentChildren, Input, OnInit} from '@angular/core';
import {SearchFieldComponent} from "../search-field.component";
import {ProductParamsBuilderService} from "../../../core/services/product-params-builder.service";
import {BookService} from "../../../core/services/book.service";
import {RelatedEntityDto} from "../../../core/DTOs/related-entity-dto";
import {map} from "rxjs/operators";
import {BehaviorSubject, Observable, Subject} from "rxjs";
import {Entity} from "../../../core/models/entity";
import {HttpClient} from "@angular/common/http";
import {EntityDto} from "../../../core/DTOs/entity-dto";

@Component({
  selector: 'app-search-hint',
  templateUrl: './search-hint.component.html',
  styleUrls: ['./search-hint.component.css'],
  providers: [ProductParamsBuilderService]
})
export class SearchHintComponent implements OnInit {

  public entities$: Subject<EntityDto[]> = new Subject();

  @Input() target: string | undefined;
  @Input() uri: string | undefined;

  @ContentChild(Text) targetText!: Text;

  constructor(
    public readonly searchField: SearchFieldComponent,
    private readonly paramsBuilder: ProductParamsBuilderService,
    private readonly http: HttpClient) { }

  ngOnInit(): void {
    this.paramsBuilder.onParamsChanged = params =>
      this.http.get<EntityDto[]>(this.uri!, { params }).subscribe(this.entities$);

    this.searchField.input
      .subscribe(input => this.uploadHints(input));
  }

  private uploadHints(input: string) {
    if (input) {
      this.paramsBuilder.searchOptions$.next({
        propertyName: 'name',
        value: input,
        searchDepth: 3
      })
    }
    else {
      this.entities$.next([]);
    }
  }

  public chooseHint(searchValue: string) {
    this.searchField.redirectToSearchPage(this.target!, `(${searchValue})`);
  }
}
