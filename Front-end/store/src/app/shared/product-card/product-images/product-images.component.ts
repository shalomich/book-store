import { Component, Input, OnInit } from '@angular/core';

import { Image } from '../../../core/interfaces/image';

@Component({
  selector: 'app-product-images',
  templateUrl: './product-images.component.html',
  styleUrls: ['./product-images.component.css'],
})
export class ProductImagesComponent implements OnInit {

  @Input()
  public titleImage: Image = {} as Image;

  @Input()
  public notTitleImages: Image[] = [];

  public constructor() { }

  public ngOnInit(): void {
  }

}
