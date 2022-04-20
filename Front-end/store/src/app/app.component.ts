import { Component, OnInit } from '@angular/core';

import { ProfileProviderService } from './core/services/profile-provider.service';
import { AuthorizationDataProvider } from './core/services/authorization-data.provider';
import { TokenValidationService } from './core/services/token-validation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

  public title = 'store';

  constructor() { }

  public ngOnInit() {
  }
}
