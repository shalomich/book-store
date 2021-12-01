import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PaginationInstance } from 'ngx-pagination';

import { BehaviorSubject, config } from 'rxjs';

import { PAGE_NUMBER, PAGE_SIZE } from '../../core/utils/values';
import { PaginationOptions } from '../../core/interfaces/pagination-options';

@Component({
  selector: 'pagination-panel',
  templateUrl: './pagination-panel.component.html',
  styleUrls: ['./pagination-panel.component.css'],
})
export class PaginationPanelComponent implements OnInit {

  @Input() paginationOptions$: BehaviorSubject<PaginationOptions> = new BehaviorSubject<PaginationOptions>({
    pageNumber: 1,
    pageSize: PAGE_SIZE,
  });

  @Input() pageCount$: BehaviorSubject<number | undefined> = new BehaviorSubject<number | undefined>(undefined);

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

    this.pageCount$
      .asObservable()
      .subscribe(pageCount => {
        this.config.totalItems = pageCount;
        this.config.currentPage = 1;
      });
  }
}
