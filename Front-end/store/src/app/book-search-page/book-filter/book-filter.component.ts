import {Component, Input, OnInit} from '@angular/core';
import {FilterOptions} from '../../core/interfaces/filter-options';
import {Subject} from 'rxjs';

@Component({
  selector: 'app-book-filter',
  templateUrl: './book-filter.component.html',
  styleUrls: ['./book-filter.component.css']
})
export class BookFilterComponent implements OnInit {

  @Input()
  public filterOptions$: Subject<FilterOptions> = new Subject<FilterOptions>();

  @Input()
  public onApply: () => void;

  public constructor() {
    this.onApply = this.onApplyFilters.bind(this);
  }

  public ngOnInit(): void {
  }

  public onApplyFilters() {
    this.onApply();
  }
}
