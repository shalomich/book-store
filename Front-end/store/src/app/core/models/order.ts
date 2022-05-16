import { BasketProduct } from './basket-product';

export class Order {
  public id: number;

  public state: string;

  public userName: string;

  public email: string;

  public phoneNumber: string;

  public address: string;

  public orderReceiptMethod: string;

  public paymentMethod: string;

  public placedDate: string;

  public deliveredDate: string;

  public products: BasketProduct[];


  constructor(order: Order) {
    this.id = order.id;
    this.state = order.state;
    this.userName = order.userName;
    this.email = order.email;
    this.phoneNumber = order.phoneNumber;
    this.address = order.address;
    this.orderReceiptMethod = order.orderReceiptMethod;
    this.paymentMethod = order.paymentMethod;
    this.placedDate = order.placedDate;
    this.deliveredDate = order.deliveredDate;
    this.products = order.products;
  }
}
