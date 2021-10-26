import { Component, Input, OnInit } from '@angular/core';

import { Base64Image } from '../../../core/interfaces/base64-image';

@Component({
  selector: 'app-product-images',
  templateUrl: './product-images.component.html',
  styleUrls: ['./product-images.component.css'],
})
export class ProductImagesComponent implements OnInit {

  @Input()
  public titleImage: Base64Image = {} as Base64Image;

  @Input()
  public notTitleImages: Base64Image[] = [];

  public constructor() { }

  public ngOnInit(): void {
  }

}
