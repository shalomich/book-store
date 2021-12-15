import {Component, Input, OnInit} from '@angular/core';
import {BasketProduct} from '../../core/interfaces/basket-product';
import {BasketService} from '../../core/services/basket.service';

@Component({
  selector: 'app-basket-element',
  templateUrl: './basket-element.component.html',
  styleUrls: ['./basket-element.component.css']
})
export class BasketElementComponent implements OnInit {

  @Input()
  public product: BasketProduct = {} as BasketProduct;

  constructor(public readonly basketService: BasketService) { }

  ngOnInit(): void {
  }

}
