import { Component, OnInit } from '@angular/core';

import { ProfileService } from './core/services/profile.service';
import { AuthorizationDataProvider } from './core/services/authorization-data.provider';
import { TokenValidationService } from './core/services/token-validation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

  public title = 'store';

  constructor(
    private readonly profileService: ProfileService,
    private readonly authorizationDataProvider: AuthorizationDataProvider,
    private readonly tokenValidationService: TokenValidationService,
  ) { }

  public ngOnInit() {
    this.tokenValidationService.isTokenValid().subscribe(isValid => {
      if (isValid) {
        this.profileService.getUserProfile().subscribe();
      }
    });
  }
}
