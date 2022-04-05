import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, UrlSerializer } from '@angular/router';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-telegram-redirect',
  templateUrl: './telegram-redirect.component.html',
  styleUrls: ['./telegram-redirect.component.css'],
})
export class TelegramRedirectComponent implements OnInit {

  constructor(private readonly activatedRoute: ActivatedRoute, private readonly router: Router, private serializer: UrlSerializer) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(data => {
      const queryParamsString = new HttpParams({ fromObject: data }).toString();
      window.open(`tg:resolve?${queryParamsString}`, '_self');
    });
  }

}
