import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationDataProvider {
  public token: string | undefined;

  public get userId(): string {
    return this.parseToken().id;
  }

  public get userRole(): string {
    return this.parseToken().role;
  }

  public isAuthorized(): boolean {
    return this.token !== undefined;
  }

  private parseToken(): any {
    if (!this.token) {
      throw 'Token is not defined';
    }

    const [, payload] = this.token.split('.');

    return JSON.parse(atob(payload));
  }
}
