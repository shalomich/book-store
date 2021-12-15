import { Component, OnInit } from '@angular/core';
import {BasketService} from '../core/services/basket.service';

@Component({
  selector: 'app-basket-page',
  templateUrl: './basket-page.component.html',
  styleUrls: ['./basket-page.component.css']
})
export class BasketPageComponent implements OnInit {

  constructor(public readonly basketService: BasketService) { }

  ngOnInit(): void {
  }

}
