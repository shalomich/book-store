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

  private readonly url :string = '/book-store/catalog/book';

  public input: BehaviorSubject<string> = new BehaviorSubject<string>('');

  constructor(
    private readonly router: Router) { }

  ngOnInit(): void {
  }

  public onInputChanged(event: Event) {
    const newInput = ((event.target as any).value) as string;

    this.input.next(newInput);
  }

  public redirectToSearchPage(target: string, searchValue: string) {
    this.router.navigate([this.url], {queryParams: {target, searchValue}});
  }
}
