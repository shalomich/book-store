import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {PaginationInstance} from 'ngx-pagination';

import {BehaviorSubject} from 'rxjs';

import {PAGE_SIZE} from '../../core/utils/values';
import {PaginationOptions} from '../../core/interfaces/pagination-options';

@Component({
  selector: 'pagination-panel',
  templateUrl: './pagination-panel.component.html',
  styleUrls: ['./pagination-panel.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class PaginationPanelComponent implements OnInit {

  @Input() paginationOptions$: BehaviorSubject<PaginationOptions> = new BehaviorSubject<PaginationOptions>({
    pageNumber: 1,
    pageSize: PAGE_SIZE,
  });

  public config: PaginationInstance = {
    id: 'paginationPanel',
    currentPage: 0,
    itemsPerPage: 0,
    totalItems: 0,
  };

  public onPageChanged(number: number): void {

    if (number === this.paginationOptions$.value.pageNumber) {
      return;
    }

    this.config.currentPage = number;

    this.paginationOptions$.next({
      pageSize: PAGE_SIZE,
      pageNumber: number,
    });
  }

  ngOnInit(): void {
    const { pageNumber, pageSize } = this.paginationOptions$.value;

    this.config.currentPage = pageNumber;
    this.config.itemsPerPage = pageSize;
    this.config.totalItems = 10;
  }
}
