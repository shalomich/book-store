import {Component, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-preview-skeleton',
  templateUrl: './preview-skeleton.component.html',
  styleUrls: ['./preview-skeleton.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class PreviewSkeletonComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
