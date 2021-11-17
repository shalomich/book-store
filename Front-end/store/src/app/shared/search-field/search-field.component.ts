import { Component, OnInit } from '@angular/core';
import {BookService} from "../../core/services/book.service";
import {ProductParamsBuilderService} from "../../core/services/product-params-builder.service";
import {Router} from "@angular/router";
import {PRODUCT_URL} from "../../core/utils/values";
import {BehaviorSubject} from "rxjs";
import {newArray} from "@angular/compiler/src/util";

@Component({
  selector: 'app-search-field',
  templateUrl: './search-field.component.html',
  styleUrls: ['./search-field.component.css']
})
export class SearchFieldComponent implements OnInit {

  private readonly searchUrlTemplate: string = '/book-store/catalog/book';
  private readonly defaultTarget: string = 'name';

  public input: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(
    private readonly router: Router) { }

  ngOnInit(): void {
  }

  public buildSearchUrl(target: string, searchValue: string): string {
    if (!target || !searchValue)
      return this.searchUrlTemplate;

    return this.router
      .createUrlTree([this.searchUrlTemplate], {queryParams: {target, searchValue}})
      .toString();
  }

  public onInputChanged(event: Event) {
    const newInput = ((event.target as any).value) as string;

    this.input.next(newInput);
  }

  public redirectToSearchPage(){
    window.location.href = this.buildSearchUrl(this.defaultTarget, this.input.value);
  }
}
