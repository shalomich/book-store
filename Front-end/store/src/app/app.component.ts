import { Component, OnInit } from '@angular/core';

import { ProfileService } from './core/services/profile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

  public title = 'store';

  constructor(private readonly profileService: ProfileService) {}

  public ngOnInit() {
    this.profileService.getUserPofile();
  }
}
