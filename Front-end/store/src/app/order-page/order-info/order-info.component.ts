import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {BasketProduct} from '../../core/models/basket-product';

@Component({
  selector: 'app-order-info',
  templateUrl: './order-info.component.html',
  styleUrls: ['./order-info.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class OrderInfoComponent implements OnInit {

  @Input()
  public orderProducts: BasketProduct[] = [];

  @Input()
  public placedOrder: boolean = false;

  public readonly displayedColumns=['name', 'quantity', 'cost', 'total'];

  constructor() { }

  ngOnInit(): void {
    console.log(this.placedOrder)
  }
}
